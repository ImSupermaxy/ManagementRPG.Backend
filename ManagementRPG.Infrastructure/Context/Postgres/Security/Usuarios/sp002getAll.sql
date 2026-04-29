CREATE OR REPLACE FUNCTION sp002getAll()
RETURNS TABLE(id INT, nome VARCHAR, email varchar, arroba varchar, senhahash varchar, status SMALLINT, userinsid int, usermodid int, userinsdata TIMESTAMP, usermoddata TIMESTAMP)
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT *
    FROM tbl_002_usuario t002;
END;
$$;

SELECT * FROM sp002getAll();