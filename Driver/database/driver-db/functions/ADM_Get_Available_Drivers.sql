-- FUNCTION: public.ADM_Get_Available_Drivers(uuid)

-- DROP FUNCTION IF EXISTS public."ADM_Get_Available_Drivers"(uuid);

CREATE OR REPLACE FUNCTION public."ADM_Get_Available_Drivers"(
	userid uuid)
    RETURNS TABLE("RentID" uuid) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
    IF EXISTS (SELECT 1 FROM "ManagerUsers" AS MU WHERE MU."ID" = userid) THEN
		RETURN QUERY
			SELECT RE."ID" AS "RentID" FROM "Rents" AS RE
			LEFT JOIN "DeliveryOrders" AS DOS ON DOS."RentID" = RE."ID"
			WHERE RE."PricePaid" IS NULL AND (DOS."RentID" != RE."ID" OR DOS."RentID" IS NULL);

    END IF;

END;
$BODY$;

ALTER FUNCTION public."ADM_Get_Available_Drivers"(uuid)
    OWNER TO postgres;
