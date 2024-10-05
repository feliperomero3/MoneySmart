/*
 * To retrieve the columns metadata, including the column type for each column for all tables
 * you can query the INFORMATION_SCHEMA.COLUMNS view.
 * This query will give you a comprehensive list of all columns in all tables along with their data types
 * and other relevant metadata.
*/
SELECT
    TABLE_NAME,
    COLUMN_NAME,
    DATA_TYPE,
    CHARACTER_MAXIMUM_LENGTH,
    IS_NULLABLE
FROM
    INFORMATION_SCHEMA.COLUMNS
ORDER BY
    TABLE_NAME,
    COLUMN_NAME;
