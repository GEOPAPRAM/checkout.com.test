USE `paymentgateway`;

-- Add column payments.Transaction
SELECT COUNT(*)
INTO @columnexists
FROM information_schema.columns
WHERE TABLE_SCHEMA = 'paymentgateway'
AND TABLE_NAME = 'payments'
AND COLUMN_NAME = 'Transaction';
SET @query = IF (@columnexists > 0,
    'Select "Transaction column already exists"',
    'ALTER TABLE `payments` ADD COLUMN `Transaction` char(255) NULL DEFAULT NULL AFTER `PaymentStatus`;');
PREPARE stmt FROM @query;
EXECUTE stmt;

-- Add column payments.RejectionReasons
SELECT COUNT(*)
INTO @columnexists
FROM information_schema.columns
WHERE TABLE_SCHEMA = 'paymentgateway'
AND TABLE_NAME = 'payments'
AND COLUMN_NAME = 'RejectionReasons';
SET @query = IF (@columnexists > 0,
    'Select "SentimentMagnitude column already exists"',
    'ALTER TABLE `payments` ADD COLUMN `RejectionReasons` text NULL DEFAULT NULL AFTER `Transaction`;');
PREPARE stmt FROM @query;
EXECUTE stmt;