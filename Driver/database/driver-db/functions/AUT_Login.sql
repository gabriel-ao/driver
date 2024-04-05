-- FUNCTION: public.AUT_Login(character varying, character varying)

-- DROP FUNCTION IF EXISTS public."AUT_Login"(character varying, character varying);

CREATE OR REPLACE FUNCTION public."AUT_Login"(
	usedate character varying,
	password character varying)
    RETURNS TABLE("UseID" uuid, "Message" character varying, "Error" boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
    "Message" character varying := '';
    "Error" boolean := false;
    "UseID" uuid;
BEGIN

	IF POSITION('@' IN useDate) > 0 THEN
	
	    IF EXISTS (SELECT 1 FROM "ManagerUsers" AS MU WHERE MU."Email" = useDate AND MU."Password" = password AND MU."Active" = true) THEN
	
			"UseID" := (SELECT MUS."ID" FROM "ManagerUsers" AS MUS WHERE  MUS."Email" = useDate);
	
	    ELSE 
	        "Message" := 'Invalid user';
	        "Error" := TRUE;
	    END IF;
	ELSE
	    IF EXISTS (SELECT 1 FROM "Drivers" AS DR WHERE DR."CnhNumber" = useDate AND DR."Password" = password AND DR."Active" = true) THEN
	
			"UseID" := (SELECT DRS."ID" FROM "Drivers" AS DRS WHERE DRS."CnhNumber" = useDate);
	
	    ELSE 
	        "Message" := 'Invalid user';
	        "Error" := TRUE;
	    END IF;
	END IF;

    RETURN QUERY
        SELECT "UseID", "Message", "Error";
END;
$BODY$;

ALTER FUNCTION public."AUT_Login"(character varying, character varying)
    OWNER TO postgres;
