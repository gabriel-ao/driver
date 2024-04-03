-- FUNCTION: public.ADM_Create_Delivery_Order(character varying, character varying, numeric, uuid)

-- DROP FUNCTION IF EXISTS public."ADM_Create_Delivery_Order"(character varying, character varying, numeric, uuid);

CREATE OR REPLACE FUNCTION public."ADM_Create_Delivery_Order"(
	title character varying,
	description character varying,
	price numeric,
	userid uuid)
    RETURNS TABLE("OrderID" uuid, "Message" character varying, "Error" boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
    "Message" character varying := '';
    "Error" boolean := false;
 	"DisponibleStatus" uuid := '7292e427-226c-4195-ad0d-c4e4f91dc987';
 	"OrderID" uuid;
BEGIN
    IF EXISTS (SELECT 1 FROM "ManagerUsers" AS MU WHERE MU."ID" = userid) THEN

		"OrderID" := uuid_generate_v4();

		INSERT INTO "DeliveryOrders" ("ID", "Title", "Description", "StatusID", "Price")
		VALUES ("OrderID", title, description, "DisponibleStatus", price);

    ELSE 
        "Message" := 'Invalid user';
        "Error" := TRUE;
    END IF;

    RETURN QUERY
        SELECT "OrderID", "Message", "Error";
END;
$BODY$;

ALTER FUNCTION public."ADM_Create_Delivery_Order"(character varying, character varying, numeric, uuid)
    OWNER TO postgres;
