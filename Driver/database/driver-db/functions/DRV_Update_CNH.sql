-- FUNCTION: public.DRV_Update_CNH(character varying, uuid)

-- DROP FUNCTION IF EXISTS public."DRV_Update_CNH"(character varying, uuid);

CREATE OR REPLACE FUNCTION public."DRV_Update_CNH"(
	urlimage character varying,
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

    IF EXISTS (SELECT 1 FROM "Drivers" AS DR WHERE DR."ID" = userid AND DR."Active" = true) THEN
		UPDATE "Drivers" SET "CnhImage" = urlimage WHERE "ID" = userid;
    ELSE 
        "Message" := 'Invalid driver';
        "Error" := TRUE;
    END IF;

    RETURN QUERY
        SELECT "Message", "Error";
END;
$BODY$;

ALTER FUNCTION public."DRV_Update_CNH"(character varying, uuid)
    OWNER TO postgres;
