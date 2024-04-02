CREATE OR REPLACE FUNCTION public."ADM_Create_Vehicle"(
    year integer,
    model character varying,
    plate character varying,
    userid uuid)
RETURNS TABLE("Message" character varying, "Error" boolean) 
LANGUAGE 'plpgsql'
COST 100
VOLATILE PARALLEL UNSAFE
ROWS 1000
AS $BODY$
DECLARE
    "Message" character varying := '';
    "Error" boolean := false;
BEGIN
    IF EXISTS (SELECT 1 FROM "ManagerUsers" AS MU WHERE MU."ID" = userid) THEN
        IF NOT EXISTS(SELECT 1 FROM "Vehicles" AS VH WHERE VH."Plate" = plate) THEN
            INSERT INTO "Vehicles" ("Plate", "Model", "Year", "Active")
            VALUES (plate, model, year, true);
        ELSE
            "Message" := 'Vehicle exists';
            "Error" := TRUE;
        END IF;
    ELSE 
        "Message" := 'Invalid user';
        "Error" := TRUE;
    END IF;

    RETURN QUERY
        SELECT "Message", "Error";
END;
$BODY$;

ALTER FUNCTION public."ADM_Create_Vehicle"(integer, character varying, character varying, uuid)
    OWNER TO postgres;
