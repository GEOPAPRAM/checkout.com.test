CREATE DATABASE IF NOT EXISTS `paymentgateway`;
USE `paymentgateway`;

CREATE TABLE IF NOT EXISTS `merchants` (
    `MerchantId` char(36) NOT NULL,
    `CompanyName` varchar(255) NOT NULL,
    `ClientName` varchar(255) NOT NULL,
    `Password` varchar(255) NOT NULL,
    PRIMARY KEY (`MerchantId`),
    KEY `CompanyName` (`CompanyName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


CREATE TABLE IF NOT EXISTS `payments` (
    `PaymentId` char(36) NOT NULL,
    `PaymentDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `Amount` decimal(10,2) NOT NULL,
    `CurrencyCode` char(3) NOT NULL DEFAULT 'GBP',
    `CardNumber` char(19) NOT NULL,
    `CVV` char(4) NOT NULL,
    `ExpirationMonth` int(2) NOT NULL,
    `ExpirationYear` int(4) NOT NULL,
    `PaymentStatus` int(1) NOT NULL DEFAULT 0,
    `MerchantId` char(36) NOT NULL,
    PRIMARY KEY (`PaymentId`),
    KEY `PaymentDate` (`PaymentDate`),
    FOREIGN KEY (MerchantId)
        REFERENCES merchants(MerchantId)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


INSERT INTO `Merchants` (`MerchantId`, `CompanyName`, `ClientName`, `Password`)
VALUES ('2f17ac7b-7e31-449b-aad0-451141663c28', 'Checkout', 'CheckoutUser', 'CheckoutPass');