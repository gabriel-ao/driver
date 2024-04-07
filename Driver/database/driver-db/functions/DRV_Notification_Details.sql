-- FUNCTION: public.DRV_Notification_Details(uuid, uuid)

-- DROP FUNCTION IF EXISTS public."DRV_Notification_Details"(uuid, uuid);

CREATE OR REPLACE FUNCTION public."DRV_Notification_Details"(
	orderid uuid,
	userid uuid)
    RETURNS TABLE("OrderID" uuid, "Title" character varying, "Description" character varying, createdate timestamp without time zone) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN

    IF EXISTS (
		SELECT 1 FROM "Drivers" AS DR
		INNER JOIN "Rents" AS RE ON RE."DriverID" = DR."ID"
		INNER JOIN "DriversNotifications" AS DNS ON DNS."RentID" = RE."ID"
		INNER JOIN "Notifications" AS NTS ON NTS."ID" = DNS."NotificationID"
		WHERE DR."ID" = userid AND NTS."OrderID" = orderid
		) THEN
		IF EXISTS (
			SELECT 1 FROM "DriversNotifications" AS DNS
			INNER JOIN "Notifications" AS NTS ON NTS."ID" = DNS."NotificationID"
			WHERE NTS."OrderID" = orderid AND DNS."Read" = false
		) THEN

			UPDATE "DriversNotifications" SET "Read" = true WHERE "ID" = (
				SELECT DNS."ID" FROM "DriversNotifications" AS DNS
				INNER JOIN "Notifications" AS NTS ON NTS."ID" = DNS."NotificationID"
				WHERE NTS."OrderID" = orderid AND DNS."Read" = false);
    	END IF;
   		
		RETURN QUERY
			SELECT NTS."OrderID", NTS."Title", NTS."Description", NTS."CreateDate" FROM "Notifications" AS NTS WHERE NTS."OrderID" = orderid;
    END IF;

END;
$BODY$;

ALTER FUNCTION public."DRV_Notification_Details"(uuid, uuid)
    OWNER TO postgres;
