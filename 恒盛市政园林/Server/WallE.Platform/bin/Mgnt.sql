/*
Navicat MySQL Data Transfer

Source Server         : bstar
Source Server Version : 50627
Source Host           : localhost:3306
Source Database       : bstar

Target Server Type    : MYSQL
Target Server Version : 50627
File Encoding         : 65001

Date: 2015-12-10 11:43:37
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for product
-- ----------------------------
DROP TABLE IF EXISTS `product`;
CREATE TABLE `product` (
  `Id` varchar(36) NOT NULL,
  `Title` varchar(255) NOT NULL,
  `Description` varchar(1000) DEFAULT NULL,
  `Content` longtext DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `ImageSmall` varchar(255) DEFAULT NULL,
  `ImageNormal` varchar(255) DEFAULT NULL,
  `Type` varchar(36) NOT NULL,
  `Author` varchar(255) DEFAULT NULL,
  `UserId` int(11) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `DisplayType` int(11) DEFAULT NULL,
  `DisplayOrder` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for category
-- ----------------------------
DROP TABLE IF EXISTS `category`;
CREATE TABLE `category` (
  `Id` varchar(36) NOT NULL,
  `Name` text,
  `Type` int(11) DEFAULT NULL COMMENT '类型（1-Product2-News3-Banner4-Other）',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of category
-- ----------------------------
INSERT INTO `category` VALUES ('0a27477c-6fe8-40ab-84fb-a9638f071c17', '园林绿化', '1');
INSERT INTO `category` VALUES ('2f0c7fc6-d862-4911-8aed-d4ff0f785c0c', '园林古建', '1');
INSERT INTO `category` VALUES ('6c04f618-50e2-4109-a4b9-bce8e3aabd64', '城市及道路照明', '1');
INSERT INTO `category` VALUES ('d5dfdce4-d34c-4a8e-bb69-69407ee82de2', '环境保护', '1');
INSERT INTO `category` VALUES ('e3dca49f-3da5-46c1-b7ef-a0106ae9f796', '市政工程', '1');
INSERT INTO `category` VALUES ('ef1375e1-d224-458a-a893-0329448e04e3', '公司新闻', '2');
INSERT INTO `category` VALUES ('77e9ad01-55c2-4e87-b9b1-e18b789bdb7e', '行业新闻', '2');
INSERT INTO `category` VALUES ('c7e66f02-3e9d-4c26-9406-a069453747e7', '通知公告', '2');

-- ----------------------------
-- Table structure for news
-- ----------------------------
DROP TABLE IF EXISTS `news`;
CREATE TABLE `news` (
  `Id` varchar(36) NOT NULL,
  `Title` varchar(255) NOT NULL,
  `Description` varchar(1000) DEFAULT NULL,
  `Content` longtext NOT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `ImageSmall` varchar(255) DEFAULT NULL,
  `ImageNormal` varchar(255) DEFAULT NULL,
  `Type` varchar(36) DEFAULT NULL,
  `Author` varchar(255) DEFAULT NULL,
  `UserId` int(11) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `DisplayType` int(11) DEFAULT NULL,
  `DisplayOrder` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for banner
-- ----------------------------
DROP TABLE IF EXISTS `banner`;
CREATE TABLE `banner` (
  `Id` varchar(36) NOT NULL,
  `Title` text,
  `Description` varchar(1000) DEFAULT NULL,
  `LinkUrl` text,
  `CreateTime` datetime DEFAULT NULL,
  `ImageSmall` varchar(255) DEFAULT NULL,
  `ImageNormal` varchar(255) DEFAULT NULL,
  `Type` varchar(36) DEFAULT NULL,
  `Author` varchar(255) DEFAULT NULL,
  `UserId` int(11) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `DisplayType` int(11) DEFAULT NULL,
  `DisplayOrder` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for banner
-- ----------------------------
DROP TABLE IF EXISTS `picture_info`;
CREATE TABLE `picture_info` (
  `Id` varchar(36) NOT NULL,
  `Name` varchar(1000) DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `ImageSmall` varchar(255) DEFAULT NULL,
  `ImageNormal` varchar(255) DEFAULT NULL,
  `ObjectId` varchar(36) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
