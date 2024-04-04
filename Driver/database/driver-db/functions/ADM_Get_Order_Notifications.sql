-- FUNCTION: public.ADM_Get_Order_Notifications(uuid, uuid)

-- DROP FUNCTION IF EXISTS public."ADM_Get_Order_Notifications"(uuid, uuid);

CREATE OR REPLACE FUNCTION public."ADM_Get_Order_Notifications"(
	orderid uuid,
	userid uuid)
    RETURNS TABLE("ID" uuid, "DriverName" character varying, "CnhNumber" character varying, "Read" boolean, "CreateDate" timestamp without time zone) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
BEGIN

    IF EXISTS (SELECT 1 FROM "ManagerUsers" AS MU WHERE MU."ID" = userid) THEN
		IF EXISTS (SELECT 1 FROM "DeliveryOrders" AS ORD WHERE ORD."ID" = orderid) THEN

			RETURN QUERY	
				SELECT 
					DNT."ID" AS "ID",
					CAST(CONCAT(DRV."FirstName", ' ', DRV."LastName") AS VARCHAR) AS "DriverName",
					DRV."CnhNumber" AS "CnhNumber",
					DNT."Read" AS "Read",
					DNT."CreateDate" AS "CreateDate"
				FROM "Notifications" AS NTF  
				INNER JOIN "DriversNotifications" AS DNT ON DNT."NotificationID" = NTF."ID"
				INNER JOIN "Rents" AS RE ON RE."ID" = DNT."RentID"
				INNER JOIN "Drivers" AS DRV ON DRV."ID" = RE."DriverID"
				WHERE NTF."OrderID" = orderid;

		ELSE 
			raise notice 'Invalid order';
		END IF;
    ELSE 
        raise notice  'Invalid user';
    END IF;
END;
$BODY$;

ALTER FUNCTION public."ADM_Get_Order_Notifications"(uuid, uuid)
    OWNER TO postgres;
