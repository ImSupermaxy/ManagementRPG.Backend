CREATE OR REPLACE FUNCTION sp100getAll()
RETURNS TABLE(id INT, mestreid INT, nome VARCHAR, descricao VARCHAR, sinopse text, status SMALLINT, userinsid int, usermodid int, userinsdata TIMESTAMP, usermoddata TIMESTAMP)
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT *
    FROM tbl_100_campanha t100;
END;
$$;

SELECT * FROM sp100getAll();