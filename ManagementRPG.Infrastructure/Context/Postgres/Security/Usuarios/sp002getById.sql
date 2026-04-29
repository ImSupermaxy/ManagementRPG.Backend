CREATE OR REPLACE FUNCTION sp002getbyid(p_id int)
RETURNS TABLE(id INT, nome VARCHAR, email varchar, arroba varchar, senhahash varchar, status SMALLINT, userinsid int, usermodid int, userinsdata TIMESTAMP, usermoddata TIMESTAMP)
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT *
    FROM tbl_002_usuario t002
    WHERE t002.id = COALESCE(p_id, t002.id);
END;
$$;

SELECT * FROM sp002getbyid(null);