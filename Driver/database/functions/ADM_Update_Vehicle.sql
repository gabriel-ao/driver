-- FUNCTION: public.ADM_Update_Vehicle(character varying, uuid, uuid)

-- DROP FUNCTION IF EXISTS public."ADM_Update_Vehicle"(character varying, uuid, uuid);

CREATE OR REPLACE FUNCTION public."ADM_Update_Vehicle"(
	newplate character varying,
	vehicleid uuid,
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
		IF NOT EXISTS (SELECT 1 FROM "Rents" WHERE "VehicleID" = vehicleid AND "PricePaid" IS NULL) THEN
        	IF NOT EXISTS(SELECT 1 FROM "Vehicles" AS VH WHERE VH."Plate" = newplate) THEN
				UPDATE "Vehicles" SET "Plate" = newplate, "UpdateDate" = CURRENT_TIMESTAMP WHERE "ID" = vehicleid;
			ELSE
				"Message" := 'Plate exist';
				"Error" := TRUE;
			END IF;
		ELSE
			"Message" := 'Vehicle rented';
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

ALTER FUNCTION public."ADM_Update_Vehicle"(character varying, uuid, uuid)
    OWNER TO postgres;
