CREATE OR REPLACE FUNCTION sp001getbyid(p_id int)
RETURNS TABLE(id INT, nome VARCHAR, versao VARCHAR, status SMALLINT, userinsid int, usermodid int, userinsdata TIMESTAMP, usermoddata TIMESTAMP)
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT *
    FROM tbl_001_sistema t001
    WHERE t001.id = COALESCE(p_id, t001.id);
END;
$$;

SELECT * FROM sp001getbyid(null);