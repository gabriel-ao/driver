-- FUNCTION: public.DRV_Get_Notifications(uuid)

-- DROP FUNCTION IF EXISTS public."DRV_Get_Notifications"(uuid);

CREATE OR REPLACE FUNCTION public."DRV_Get_Notifications"(
	userid uuid)
    RETURNS TABLE("OrderID" uuid, "Title" character varying, "DeliveryStatus" character varying, "Read" boolean, "CreateDate" timestamp without time zone) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN

    IF EXISTS (SELECT 1 FROM "Drivers" AS DR WHERE DR."ID" = userid AND DR."Active" = true) THEN
   		
		RETURN QUERY
			SELECT 
				DOS."ID" AS "OrderID",
				NTS."Title" AS "Title",
				CASE WHEN DOS."RentID" IS NULL THEN 'Available'::character varying ELSE 'Unavailable'::character varying END AS "DeliveryStatus",
				DNS."Read" AS "Read",
				NTS."CreateDate" AS "CreateDate"
			FROM "DriversNotifications" AS DNS
			INNER JOIN "Rents" AS RE ON RE."ID" = DNS."RentID"
			INNER JOIN "Notifications" AS NTS ON NTS."ID" = DNS."NotificationID"
			INNER JOIN "DeliveryOrders" AS DOS ON DOS."ID" = NTS."OrderID"
			WHERE 
				RE."DriverID" = userid
				AND RE."PricePaid" IS NULL
				ORDER BY NTS."CreateDate" DESC;
    END IF;

END;
$BODY$;

ALTER FUNCTION public."DRV_Get_Notifications"(uuid)
    OWNER TO postgres;
