CREATE OR REPLACE FUNCTION sp001getAll()
RETURNS TABLE(id INT, nome VARCHAR, versao VARCHAR, status SMALLINT, userinsid int, usermodid int, userinsdata TIMESTAMP, usermoddata TIMESTAMP)
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT *
    FROM tbl_001_sistema t001;
END;
$$;

SELECT * FROM sp001getAll();