-- FUNCTION: public.DRV_Create_Rent(uuid, uuid)

-- DROP FUNCTION IF EXISTS public."DRV_Create_Rent"(uuid, uuid);

CREATE OR REPLACE FUNCTION public."DRV_Create_Rent"(
	planid uuid,
	userid uuid)
    RETURNS TABLE("RentID" uuid, "Message" character varying, "Error" boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
    "RentID" uuid;
	vehicleid uuid := NULL;
    "Message" character varying := '';
    "Error" boolean := false;
	"initdate" timestamp without time zone;
	"finishdate" timestamp without time zone;
	"previousdate" timestamp without time zone;
	"pricerent" numeric(10, 2);
BEGIN

	DROP TABLE IF EXISTS temp_vehicles;
	CREATE TEMP TABLE temp_vehicles (
		"ID" UUID
	);
	
	DROP TABLE IF EXISTS temp_plan;
	CREATE TEMP TABLE temp_plan (
		"ID" UUID,
		"Days" integer,
		"Price" numeric(10, 2)
	);
				
	IF EXISTS (SELECT 1 FROM "Drivers" AS DR WHERE DR."ID" = userid AND DR."Active" = true) THEN
		IF EXISTS (SELECT 1 FROM "Drivers" AS DR WHERE DR."ID" = userid AND DR."CnhImage" <> '') THEN
			IF NOT EXISTS (SELECT 1 FROM "Rents" AS RE WHERE RE."DriverID" = userid AND RE."PricePaid" IS NULL LIMIT 1) THEN
				IF EXISTS(SELECT 1 FROM "CnhTypes" WHERE "ID" = (SELECT "CnhID" FROM "Drivers" AS DR WHERE DR."ID" = userid AND DR."Active" = true) AND "Description" LIKE '%A%') THEN
					INSERT INTO temp_vehicles
					SELECT VH."ID" 
					FROM "Vehicles" AS VH 
					INNER JOIN "Rents" AS R ON VH."ID" = R."VehicleID" AND R."PricePaid" IS NULL;
	
					vehicleid := (SELECT VHC."ID" FROM "Vehicles" AS VHC WHERE VHC."ID" NOT IN(SELECT TP."ID" FROM temp_vehicles AS TP) LIMIT 1);
	
					IF(vehicleid IS NULL) THEN
						"Message" := 'Vehicles unavailable, please try again later';
						"Error" := true;
					ELSE 
						IF EXISTS (SELECT 1 FROM "Plans" WHERE "ID" = planid) THEN
							INSERT INTO temp_plan
							SELECT "ID", "Days", "Price" FROM "Plans" WHERE "ID" = planid;
	
							"initdate" := (SELECT CURRENT_TIMESTAMP + INTERVAL '1 day');
							"finishdate" := (SELECT CURRENT_DATE + INTERVAL '1 day' + (SELECT TO_CHAR((SELECT "Days" FROM temp_plan), '999') || ' days 23 hours')::INTERVAL);
							"previousdate" := "finishdate";
	
							"pricerent" := ((SELECT "Days" FROM temp_plan) * (SELECT "Price" FROM temp_plan));
	
							"RentID" := uuid_generate_v4();
	
							INSERT INTO "Rents" ("ID", "VehicleID", "PlanID", "DriverID", "InitialRent", "PreviousRent", "FinishRent", "PriceRent")
							VALUES ("RentID", vehicleid, planid, userid, "initdate",  "previousdate", "finishdate", "pricerent");
	
						ELSE 
							"Message" := 'Invalid plan';
							"Error" := true;
						END IF;
					END IF;
				ELSE
					"Message" := 'Invalid cnh';
					"Error" := true;
				END IF;
			ELSE 
				"Message" := 'finish your last rental to start another';
				"Error" := true;
			END IF;
		ELSE
			"Message" := 'driver need cnh image to rent';
			"Error" := true;
		END IF;
	ELSE
		"Message" := 'Invalid driver';
		"Error" := true;
	END IF;
	
	DROP TABLE IF EXISTS temp_vehicles;

	
    RETURN QUERY
        SELECT "RentID", "Message", "Error";
END;
$BODY$;

ALTER FUNCTION public."DRV_Create_Rent"(uuid, uuid)
    OWNER TO postgres;
