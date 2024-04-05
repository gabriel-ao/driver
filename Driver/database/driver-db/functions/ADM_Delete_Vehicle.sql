-- FUNCTION: public.ADM_Delete_Vehicle(uuid, uuid)

-- DROP FUNCTION IF EXISTS public."ADM_Delete_Vehicle"(uuid, uuid);

CREATE OR REPLACE FUNCTION public."ADM_Delete_Vehicle"(
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
        	IF NOT EXISTS(SELECT 1 FROM "Rents" WHERE "VehicleID" = vehicleid LIMIT 1) THEN
				IF EXISTS(SELECT 1 FROM "Vehicles" WHERE "ID" = vehicleid) THEN
					DELETE from "Vehicles" WHERE "ID" = vehicleid; -- delete
				ELSE 
					"Message" := 'Invalid Vehicle';
					"Error" := TRUE;		
				END IF;
			ELSE
				UPDATE "Vehicles" SET "Deleted" = true, "UpdateDate" = CURRENT_TIMESTAMP WHERE "ID" = vehicleid; -- logic delete
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

ALTER FUNCTION public."ADM_Delete_Vehicle"(uuid, uuid)
    OWNER TO postgres;
