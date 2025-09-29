CREATE OR REPLACE FUNCTION sp001get(p_id int, p_status SMALLINT, p_versao varchar(24))
RETURNS TABLE(id INT, nome VARCHAR, versao VARCHAR, status SMALLINT, userinsid int, usermodid int, userinsdata TIMESTAMP, usermoddata TIMESTAMP)
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT *
    FROM tbl_001_sistema t001
    WHERE t001.id = COALESCE(p_id, t001.id)
	AND t001.status = COALESCE(p_status, t001.status)
	AND t001.versao = COALESCE(p_versao, t001.versao);
END;
$$;

SELECT * FROM sp001get(null, null, null);