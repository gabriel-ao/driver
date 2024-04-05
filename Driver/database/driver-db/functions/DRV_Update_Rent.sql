-- FUNCTION: public.DRV_Update_Rent(timestamp without time zone, uuid, uuid)

-- DROP FUNCTION IF EXISTS public."DRV_Update_Rent"(timestamp without time zone, uuid, uuid);

CREATE OR REPLACE FUNCTION public."DRV_Update_Rent"(
	previousdate timestamp without time zone,
	rentid uuid,
	userid uuid)
    RETURNS TABLE("FinishDate" timestamp without time zone, "Price" numeric, "Message" character varying, "Error" boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
	previousrent timestamp without time zone;
    "CalcDays" numeric;
    "PricePlan" numeric;
    "DayPlan" numeric;
    "PercentagePlan" numeric;
	"trafficTicket" numeric := 50;
	planid uuid;
    "Message" character varying := '';
    "Error" boolean := false;
BEGIN

	DROP TABLE IF EXISTS temp_rent;
	CREATE TEMP TABLE temp_rent (
		"ID" UUID,
		"PreviousRent" timestamp without time zone,
		"FinishRent" timestamp without time zone,
		"PriceRent" numeric(10, 2)
	);
	
	DROP TABLE IF EXISTS temp_plan;
	CREATE TEMP TABLE temp_plan (
		"ID" UUID,
		"Days" integer,
		"Price" numeric(10, 2)
	);

	
	IF EXISTS (SELECT 1 FROM "Drivers" AS DR WHERE DR."ID" = userid AND DR."Active" = true) THEN
		IF EXISTS (SELECT 1 FROM "Rents" AS RE WHERE RE."ID" = rentid AND RE."PricePaid" IS NULL LIMIT 1) THEN
		
			UPDATE "Rents" SET "PreviousRent" = previousdate, "UpdateDate" = CURRENT_TIMESTAMP WHERE "ID" = rentid;
			
			INSERT INTO temp_rent
			SELECT "ID", "PreviousRent", "FinishRent", "PriceRent" FROM "Rents" AS RE WHERE RE."ID" = rentid AND RE."PricePaid" IS NULL LIMIT 1;
			
			previousrent := (SELECT "PreviousRent" FROM temp_rent);
			"FinishDate" := (SELECT "FinishRent" FROM temp_rent);
			"Price" := (SELECT "PriceRent" FROM temp_rent);
			
			IF("FinishDate" > previousrent) THEN
				"CalcDays" := (SELECT EXTRACT(day FROM ("FinishDate"::timestamp - previousrent::timestamp)));
				planid := (SELECT "PlanID" FROM "Rents" AS RE WHERE RE."ID" = rentid AND RE."PricePaid" IS NULL LIMIT 1);
				
				INSERT INTO temp_plan
				SELECT PL."ID", PL."Days", PL."Price" FROM "Plans" AS PL WHERE PL."ID" = planid;
				
    			"DayPlan" := (SELECT TP."Days" FROM temp_plan AS TP);
				"PricePlan" := (SELECT TP."Price" FROM temp_plan AS TP);

				"PercentagePlan" := CASE WHEN "DayPlan" = 7 THEN 20 WHEN "DayPlan" = 15 THEN 40 WHEN "DayPlan" = 30 THEN 60 END; -- CHANGE PERCENTAGEM
				"DayPlan" := ("DayPlan" - "CalcDays");

				"Price" := ("DayPlan" * "PricePlan");
	
				"Price" := ROUND(("Price" + (("PercentagePlan" / 100.0) * ("PricePlan" * "CalcDays"))), 2);
				"FinishDate" := previousrent;

			ELSE 
				"CalcDays" := (SELECT EXTRACT(day FROM (previousrent::timestamp - "FinishDate"::timestamp)));
				"CalcDays" := ("CalcDays" * "trafficTicket");
				"Price" := ("Price" + "CalcDays");
			END IF;
			
		ELSE 
			"Message" := 'Rent not found';
			"Error" := true;
		END IF;	
	ELSE
		"Message" := 'Invalid driver';
		"Error" := true;
	END IF;
	
    RETURN QUERY
        SELECT "FinishDate", "Price",  "Message", "Error";
END;
$BODY$;

ALTER FUNCTION public."DRV_Update_Rent"(timestamp without time zone, uuid, uuid)
    OWNER TO postgres;
