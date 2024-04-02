-- FUNCTION: public.DRV_Create_Driver(character varying, character varying, character varying, date, character varying, uuid, character varying, character varying)

-- DROP FUNCTION IF EXISTS public."DRV_Create_Driver"(character varying, character varying, character varying, date, character varying, uuid, character varying, character varying);

CREATE OR REPLACE FUNCTION public."DRV_Create_Driver"(
	firstname character varying,
	lastname character varying,
	cnpj character varying,
	birthdate date,
	cnhnumber character varying,
	cnhid uuid,
	cnhimage character varying,
	password character varying)
    RETURNS TABLE("UserID" uuid, "Message" character varying, "Error" boolean) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
DECLARE
    "UserID" uuid;
    "Message" character varying := '';
    "Error" boolean := false;
	"Validate" boolean := false;
	"MinimalAge" Date;
BEGIN

	"MinimalAge" := CURRENT_DATE - INTERVAL '18 years';
	IF "MinimalAge" <= birthdate THEN
		"Message" := 'Invalid birth date';
        "Error" := true;
		"Validate" := true;
	END IF;
	
	IF NOT EXISTS(SELECT 1 FROM "CnhTypes" AS CNH WHERE CNH."ID" = cnhid) THEN
		"Message" := 'Cnh type not exist';
        "Error" := true;
		"Validate" := true;
	END IF;
	
	IF("Validate" = false) THEN
		IF NOT EXISTS (SELECT 1 FROM "Drivers" AS DR WHERE DR."CnhNumber" = cnhnumber and (DR."Active" IS NULL OR DR."Active" = true)) THEN
			"UserID" := uuid_generate_v4();
			INSERT INTO "Drivers" ("ID", "FirstName", "LastName", "CNPJ", "BirthDate", "CnhNumber", "CnhID", "CnhImage", "Password", "Active")
			VALUES ("UserID" , firstname, lastname, cnpj, birthdate, cnhnumber, cnhid, cnhimage, password, true);
		ELSE 
			"Message" := 'Driver exist';
			"Error" := TRUE;
		END IF;
	END IF;
	
    RETURN QUERY
        SELECT "UserID", "Message", "Error";
END;
$BODY$;

ALTER FUNCTION public."DRV_Create_Driver"(character varying, character varying, character varying, date, character varying, uuid, character varying, character varying)
    OWNER TO postgres;
