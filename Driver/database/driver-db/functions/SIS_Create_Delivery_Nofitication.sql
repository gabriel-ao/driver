-- FUNCTION: public.Create_Delivery_Nofitication(uuid, character varying, character varying, uuid[])

-- DROP FUNCTION IF EXISTS public."Create_Delivery_Nofitication"(uuid, character varying, character varying, uuid[]);

CREATE OR REPLACE FUNCTION public."Create_Delivery_Nofitication"(
	orderid uuid,
	title character varying,
	description character varying,
	rents uuid[])
    RETURNS TABLE("Message" character varying, "Error" boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
    "Message" character varying := '';
    "Error" boolean := false;
	"NotificationID" uuid;
BEGIN

    IF EXISTS (SELECT 1 FROM "DeliveryOrders" AS DOS WHERE DOS."ID" = orderid) THEN

		"NotificationID" := uuid_generate_v4();

		INSERT INTO "Notifications" ("ID", "Title", "Description", "OrderID")
		VALUES ("NotificationID", title, description, orderid);

		INSERT INTO "DriversNotifications" ("RentID", "NotificationID", "Read")
		SELECT unnest(rents), "NotificationID", false;

    ELSE 
        "Message" := 'Invalid order';
        "Error" := TRUE;
    END IF;

    RETURN QUERY
        SELECT "Message", "Error";
END;
$BODY$;

ALTER FUNCTION public."Create_Delivery_Nofitication"(uuid, character varying, character varying, uuid[])
    OWNER TO postgres;
