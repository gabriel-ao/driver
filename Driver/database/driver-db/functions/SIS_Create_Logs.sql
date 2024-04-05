-- FUNCTION: public.SIS_Create_Logs(character varying, character varying, character varying, character varying)

-- DROP FUNCTION IF EXISTS public."SIS_Create_Logs"(character varying, character varying, character varying, character varying);

CREATE OR REPLACE FUNCTION public."SIS_Create_Logs"(
	methodname character varying,
	message character varying,
	stackmessage character varying,
	type character varying)
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
	IF(type != 'Error') THEN
		type := 'Info';
	END IF;
	
	INSERT INTO "Logs" ("ID","MethodName", "Message", "StackMessage", "CreateDate", "Type") 
	VALUES (uuid_generate_v4(), methodname, message, StackMessage, CURRENT_TIMESTAMP, type);

    RETURN QUERY
        SELECT "Message", "Error";
END;
$BODY$;

ALTER FUNCTION public."SIS_Create_Logs"(character varying, character varying, character varying, character varying)
    OWNER TO postgres;
