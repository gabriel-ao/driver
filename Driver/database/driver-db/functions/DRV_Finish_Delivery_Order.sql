-- FUNCTION: public.DRV_Finish_Delivery_Order(uuid, uuid)

-- DROP FUNCTION IF EXISTS public."DRV_Finish_Delivery_Order"(uuid, uuid);

CREATE OR REPLACE FUNCTION public."DRV_Finish_Delivery_Order"(
	orderid uuid,
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

    IF EXISTS (SELECT 1 FROM "Drivers" AS MU WHERE MU."ID" = userid) THEN
		IF EXISTS (SELECT 1 FROM "DeliveryOrders" AS ORD WHERE ORD."ID" = orderid) THEN
			IF EXISTS (
				SELECT 1 FROM "Drivers" AS DR
					INNER JOIN "Rents" AS RE ON RE."DriverID" = DR."ID" AND RE."PricePaid" IS NULL
					INNER JOIN "DriversNotifications" AS DNS ON DNS."RentID" = RE."ID" 
					INNER JOIN "Notifications" AS NTF ON NTF."ID" = DNS."NotificationID"
					INNER JOIN "DeliveryOrders" AS DOS ON DOS."ID" = NTF."OrderID"
				WHERE DR."ID" = userid
					AND DR."Active" = true	
					AND DOS."ID" = orderid
					AND DOS."StatusID" = (SELECT PST."ID" FROM "ProcessStatus" AS PST WHERE PST."Description" = 'Aceito')
					AND DOS."RentID" IS NOT null
			) THEN

				UPDATE "DeliveryOrders" SET
					"StatusID" = (SELECT "ID" FROM "ProcessStatus" AS PST WHERE PST."Description" = 'Entregue'), 
					"UpdateDate" = CURRENT_TIMESTAMP 
				WHERE "ID" = orderid;

			ELSE 
				"Message" :='Order not available';
				"Error" := true;
			END IF;
		ELSE 
			"Message" := 'Invalid order';
			"Error" := true;
		END IF;
    ELSE 
        "Message" := 'Invalid user';
		"Error" := true;
    END IF;

    RETURN QUERY
        SELECT "Message", "Error";
	
END;
$BODY$;

ALTER FUNCTION public."DRV_Finish_Delivery_Order"(uuid, uuid)
    OWNER TO postgres;
