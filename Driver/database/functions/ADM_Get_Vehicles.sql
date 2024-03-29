-- FUNCTION: public.ADM_Get_Vehicles(character varying, uuid)

-- DROP FUNCTION IF EXISTS public."ADM_Get_Vehicles"(character varying, uuid);

CREATE OR REPLACE FUNCTION public."ADM_Get_Vehicles"(
	plate character varying,
	userid uuid)
    RETURNS TABLE("ID" uuid, "Year" integer, "Model" character varying, "Plate" character varying, "Status" character varying, "Driver" character varying) 
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
    ROWS 1000

AS $BODY$
BEGIN
	
    IF EXISTS (SELECT 1 FROM "ManagerUsers" AS MU WHERE MU."ID" = userid) THEN
        RETURN QUERY
            SELECT 
                VH."ID", 
                VH."Year", 
                VH."Model", 
                VH."Plate",
                CASE 
                    WHEN VH."Active" = false THEN 'Blocked'::character varying  
                    WHEN R."VehicleID" IS NOT NULL AND R."PricePaid" IS NULL THEN 'Rented'::character varying 
                    ELSE 'Available'::character varying 
                END AS "Status",
                DR."CnhNumber" as "Driver"
            FROM 
                "Vehicles" AS VH 
            LEFT JOIN 
                "Rents" AS R ON VH."ID" = R."VehicleID" AND R."PricePaid" IS NULL
            LEFT JOIN 
                "Drivers" AS DR ON DR."ID" = R."DriverID"
            WHERE 
                (VH."Deleted" IS NULL OR VH."Deleted" = false) AND
                VH."Plate" = CASE WHEN plate = '' THEN VH."Plate" ELSE plate END;
    ELSE
        -- Retorna uma lista vazia se o userid não existir na tabela ManagerUsers
        RETURN QUERY SELECT NULL::uuid, NULL::integer, NULL::character varying, NULL::character varying, NULL::character varying, NULL::character varying;
    END IF;
    		
END;
$BODY$;

ALTER FUNCTION public."ADM_Get_Vehicles"(character varying, uuid)
    OWNER TO postgres;
