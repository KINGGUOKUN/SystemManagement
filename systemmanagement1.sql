-- MySQL dump 10.13  Distrib 8.0.19, for Linux (x86_64)
--
-- Host: localhost    Database: SystemManagement
-- ------------------------------------------------------
-- Server version	8.0.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: `SystemManagement`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `SystemManagement` /*!40100 DEFAULT CHARACTER SET utf8 */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `SystemManagement`;

--
-- Table structure for table `CMSArticle`
--

DROP TABLE IF EXISTS `CMSArticle`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `CMSArticle` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `CreateBy` bigint DEFAULT NULL COMMENT '创建人',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间/注册时间',
  `ModifyBy` bigint DEFAULT NULL COMMENT '最后更新人',
  `ModifyTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  `Author` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '作者',
  `Content` text CHARACTER SET utf8 COLLATE utf8_general_ci COMMENT '内容',
  `IdChannel` bigint NOT NULL COMMENT '栏目id',
  `Img` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '文章题图ID',
  `Title` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '标题',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8 COMMENT='文章';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CMSArticle`
--

LOCK TABLES `CMSArticle` WRITE;
/*!40000 ALTER TABLE `CMSArticle` DISABLE KEYS */;
INSERT INTO `CMSArticle` VALUES (1,1,'2019-03-09 16:24:58',1,'2019-05-10 13:22:49','enilu','<div id=\"articleContent\" class=\"content\">\n<div class=\"ad-wrap\">\n<p style=\"margin: 0 0 10px 0;\">一般我们都有这样的需求：我需要知道库中的数据是由谁创建，什么时候创建，最后一次修改时间是什么时候，最后一次修改人是谁。web-flash最新代码已经实现该需求，具体实现方式网上有很多资料，这里做会搬运工，将web-flash的实现步骤罗列如下：%%</p>\n</div>\n<p>在Spring jpa中可以通过在实体bean的属性或者方法上添加以下注解来实现上述需求@CreatedDate、@CreatedBy、@LastModifiedDate、@LastModifiedBy。</p>\n<ul class=\" list-paddingleft-2\">\n<li>\n<p>@CreatedDate 表示该字段为创建时间时间字段，在这个实体被insert的时候，会设置值</p>\n</li>\n<li>\n<p>@CreatedBy 表示该字段为创建人，在这个实体被insert的时候，会设置值</p>\n</li>\n<li>\n<p>@LastModifiedDate 最后修改时间 实体被update的时候会设置</p>\n</li>\n<li>\n<p>@LastModifiedBy 最后修改人 实体被update的时候会设置</p>\n</li>\n</ul>\n<h2>使用方式</h2>\n<h3>实体类添加注解</h3>\n<ul class=\" list-paddingleft-2\">\n<li>\n<p>首先在实体中对应的字段加上上述4个注解</p>\n</li>\n<li>\n<p>在web-flash中我们提取了一个基础实体类BaseEntity，并将对应的字段添加上述4个注解,所有需要记录维护信息的表对应的实体都集成该类</p>\n</li>\n</ul>\n<pre>import&nbsp;org.springframework.data.annotation.CreatedBy;\nimport&nbsp;org.springframework.data.annotation.CreatedDate;\nimport&nbsp;org.springframework.data.annotation.LastModifiedBy;\nimport&nbsp;org.springframework.data.annotation.LastModifiedDate;\nimport&nbsp;javax.persistence.Column;\nimport&nbsp;javax.persistence.GeneratedValue;\nimport&nbsp;javax.persistence.Id;\nimport&nbsp;javax.persistence.MappedSuperclass;\nimport&nbsp;java.io.Serializable;\nimport&nbsp;java.util.Date;\n@MappedSuperclass\n@Data\npublic&nbsp;abstract&nbsp;class&nbsp;BaseEntity&nbsp;implements&nbsp;Serializable&nbsp;{\n&nbsp;&nbsp;&nbsp;&nbsp;@Id\n&nbsp;&nbsp;&nbsp;&nbsp;@GeneratedValue\n&nbsp;&nbsp;&nbsp;&nbsp;private&nbsp;Long&nbsp;id;\n&nbsp;&nbsp;&nbsp;&nbsp;@CreatedDate\n&nbsp;&nbsp;&nbsp;&nbsp;@Column(name&nbsp;=&nbsp;\"create_time\",columnDefinition=\"DATETIME&nbsp;COMMENT&nbsp;\'创建时间/注册时间\'\")\n&nbsp;&nbsp;&nbsp;&nbsp;private&nbsp;Date&nbsp;createTime;\n&nbsp;&nbsp;&nbsp;&nbsp;@Column(name&nbsp;=&nbsp;\"create_by\",columnDefinition=\"bigint&nbsp;COMMENT&nbsp;\'创建人\'\")\n&nbsp;&nbsp;&nbsp;&nbsp;@CreatedBy\n&nbsp;&nbsp;&nbsp;&nbsp;private&nbsp;Long&nbsp;createBy;\n&nbsp;&nbsp;&nbsp;&nbsp;@LastModifiedDate\n&nbsp;&nbsp;&nbsp;&nbsp;@Column(name&nbsp;=&nbsp;\"modify_time\",columnDefinition=\"DATETIME&nbsp;COMMENT&nbsp;\'最后更新时间\'\")\n&nbsp;&nbsp;&nbsp;&nbsp;private&nbsp;Date&nbsp;modifyTime;\n&nbsp;&nbsp;&nbsp;&nbsp;@LastModifiedBy\n&nbsp;&nbsp;&nbsp;&nbsp;@Column(name&nbsp;=&nbsp;\"modify_by\",columnDefinition=\"bigint&nbsp;COMMENT&nbsp;\'最后更新人\'\")\n&nbsp;&nbsp;&nbsp;&nbsp;private&nbsp;Long&nbsp;modifyBy;\n}</pre>\n<h3>实现AuditorAware接口返回操作人员的id</h3>\n<p>配置完上述4个注解后，在jpa.save方法被调用的时候，时间字段会自动设置并插入数据库，但是CreatedBy和LastModifiedBy并没有赋值 这两个信息需要实现AuditorAware接口来返回操作人员的id</p>\n<ul class=\" list-paddingleft-2\">\n<li>\n<p>首先需要在项目启动类添加@EnableJpaAuditing 注解来启用审计功能</p>\n</li>\n</ul>\n<pre>@SpringBootApplication\n@EnableJpaAuditing\npublic&nbsp;class&nbsp;AdminApplication&nbsp;extends&nbsp;WebMvcConfigurerAdapter&nbsp;{\n&nbsp;//省略\n}</pre>\n<ul class=\" list-paddingleft-2\">\n<li>\n<p>然后实现AuditorAware接口返回操作人员的id</p>\n</li>\n</ul>\n<pre>@Configuration\npublic&nbsp;class&nbsp;UserIDAuditorConfig&nbsp;implements&nbsp;AuditorAware&lt;Long&gt;&nbsp;{\n&nbsp;&nbsp;&nbsp;&nbsp;@Override\n&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;Long&nbsp;getCurrentAuditor()&nbsp;{\n&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ShiroUser&nbsp;shiroUser&nbsp;=&nbsp;ShiroKit.getUser();\n&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;if(shiroUser!=null){\n&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;return&nbsp;shiroUser.getId();\n&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}\n&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;return&nbsp;null;\n&nbsp;&nbsp;&nbsp;&nbsp;}\n}</pre>\n</div>',1,'1','web-flash 将所有表增加维护人员和维护时间信息'),(2,1,'2019-03-09 16:24:58',1,'2019-03-23 23:12:16','enilu.cn','<div id=\"articleContent\" class=\"content\">\n<div class=\"ad-wrap\">\n<p style=\"margin: 0 0 10px 0;\"><a style=\"color: #a00; font-weight: bold;\" href=\"https://my.oschina.net/u/3985214/blog/3018099?tdsourcetag=s_pcqq_aiomsg\" target=\"_blank\" rel=\"noopener\" data-traceid=\"news_detail_above_text_link_1\" data-tracepid=\"news_detail_above_text_link\">开发十年，就只剩下这套架构体系了！ &gt;&gt;&gt;</a>&nbsp;&nbsp;<img style=\"max-height: 32px; max-width: 32px;\" src=\"https://www.oschina.net/img/hot3.png\" align=\"\" /></p>\n</div>\n<h3>国际化</h3>\n<ul class=\" list-paddingleft-2\">\n<li>\n<p>web-flash实现国际化了.</p>\n</li>\n<li>\n<p>不了解上面两个的区别的同学可以再回顾下这个<a href=\"http://www.enilu.cn/web-flash/base/web-flash.html\">文档</a></p>\n</li>\n<li>\n<p>web-flash实现国际化的方式参考vue-element-admin的&nbsp;<a href=\"https://panjiachen.github.io/vue-element-admin-site/zh/guide/advanced/i18n.html\" target=\"_blank\" rel=\"noopener\">官方文档</a>,这里不再赘述,强烈建议你先把文档读了之后再看下面的内容。</p>\n</li>\n</ul>\n<h3>默认约定</h3>\n<p>针对网站资源进行国际园涉及到的国际化资源的管理维护，这里给出一些web-flash的资源分类建议，当然，你也可以根据你的实际情况进行调整。</p>\n<ul class=\" list-paddingleft-2\">\n<li>\n<p>src/lang/为国际化资源目录,目前提供了英文（en.js）和中文(zh.js)的两种语言实现。</p>\n</li>\n<li>\n<p>目前资源语言资源文件中是json配置主要有以下几个节点：</p>\n</li>\n<ul class=\" list-paddingleft-2\" style=\"list-style-type: square;\">\n<li>\n<p>route 左侧菜单资源</p>\n</li>\n<li>\n<p>navbar 顶部导航栏资源</p>\n</li>\n<li>\n<p>button 公共的按钮资源，比如：添加、删除、修改、确定、取消之类等等</p>\n</li>\n<li>\n<p>common 其他公共的资源，比如一些弹出框标题、提示信息、label等等</p>\n</li>\n<li>\n<p>login 登录页面资源</p>\n</li>\n<li>\n<p>config 参数管理界面资源</p>\n</li>\n</ul>\n<li>\n<p>目前针对具体的页面资源只做了登录和参数管理两个页面，其他具体业务界面仅针对公共的按钮做了国际化，你可以参考config页面资源进行配置进行进一步配置：/src/views/cfg/</p>\n</li>\n<li>\n<p>如果你有其他资源在上面对应的节点添加即可，针对每个页面特有的资源以页面名称作为几点进行维护，这样方便记忆和维护，不容易出错。</p>\n</li>\n</ul>\n<h3>添加新的语言支持</h3>\n<p>如果英文和中文两种语言不够，那么你可以通过下面步骤添加语言支持</p>\n<ul class=\" list-paddingleft-2\">\n<li>\n<p>在src/lang/目录下新增对应的资源文件</p>\n</li>\n<li>\n<p>在src/lang/index.js中import对应的资源文件</p>\n</li>\n<li>\n<p>在src/lang/index.js中的messages变量中加入新的语言声明</p>\n</li>\n<li>\n<p>在src/components/LangSelect/index.vue的语言下拉框中增加新的语言选项</p>\n</li>\n</ul>\n<h3>演示地址</h3>\n<ul class=\" list-paddingleft-2\">\n<li>\n<p>vue版本后台管理&nbsp;<a href=\"http://106.75.35.53:8082/vue/#/login\" target=\"_blank\" rel=\"noopener\">http://106.75.35.53:8082/vue/#/login</a></p>\n</li>\n<li>CMS内容管理系统的h5前端demo:<a href=\"http://106.75.35.53:8082/mobile/#/index\" target=\"_blank\" rel=\"noopener\">http://106.75.35.53:8082/mobile/#/index</a></li>\n</ul>\n</div>',1,'2','web-flash1.0.1 发布，增加国际化和定时任务管理功能'),(3,1,'2019-03-09 16:24:58',1,'2019-04-28 17:39:52','enilu.cn','<div class=\"content\" id=\"articleContent\">\r\n                        <div class=\"ad-wrap\">\r\n                                                    <p style=\"margin:0 0 10px 0;\"><a data-traceid=\"news_detail_above_text_link_1\" data-tracepid=\"news_detail_above_text_link\" style=\"color:#A00;font-weight:bold;\" href=\"https://my.oschina.net/u/3985214/blog/3018099?tdsourcetag=s_pcqq_aiomsg\" target=\"_blank\">开发十年，就只剩下这套架构体系了！ &gt;&gt;&gt;</a>&nbsp;&nbsp;<img src=\"https://www.oschina.net/img/hot3.png\" align=\"\" style=\"max-height: 32px; max-width: 32px;\"></p>\r\n                                    </div>\r\n                        <p>web-flash使用的Spring Boot从1.5.1升级到2.1.1</p><p>下面为升级过程</p><ul class=\" list-paddingleft-2\"><li><p>版本升级</p><pre>&lt;spring.boot.version&gt;2.1.1.RELEASE&lt;/spring.boot.version&gt;\r\n&lt;springframework.version&gt;5.1.3.RELEASE&lt;springframework.version&gt;</pre></li><li><p>配置增加</p><pre>spring.main.allow-bean-definition-overriding=true\r\nspring.jpa.hibernate.use-new-id-generator-mappings=false</pre></li></ul><ul class=\" list-paddingleft-2\"><li><p>审计功能调整，调整后代码:</p><pre>@Configuration\r\npublic&nbsp;class&nbsp;UserIDAuditorConfig&nbsp;implements&nbsp;AuditorAware&lt;Long&gt;&nbsp;{\r\n&nbsp;&nbsp;&nbsp;&nbsp;@Override\r\n&nbsp;&nbsp;&nbsp;&nbsp;public&nbsp;Optional&lt;Long&gt;&nbsp;getCurrentAuditor()&nbsp;{\r\n&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ShiroUser&nbsp;shiroUser&nbsp;=&nbsp;ShiroKit.getUser();\r\n&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;if(shiroUser!=null){\r\n&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;return&nbsp;Optional.of(shiroUser.getId());\r\n&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}\r\n&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;return&nbsp;null;\r\n&nbsp;&nbsp;&nbsp;&nbsp;}\r\n}</pre></li><li><p>repository调整</p></li><ul class=\" list-paddingleft-2\" style=\"list-style-type: square;\"><li><p>之前的 delete(Long id)方法没有了，替换为：deleteById(Long id)</p></li><li><p>之前的 T findOne(Long id)方法没有了，替换为：		</p><pre>Optional&lt;T&gt;&nbsp;optional&nbsp;=&nbsp;***Repository.findById(id);\r\n&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;if&nbsp;(optional.isPresent())&nbsp;{\r\n&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;return&nbsp;optional.get();\r\n&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}\r\n&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;return&nbsp;null;</pre></li></ul><li><p>随着这次Spring Boot的升级也顺便做了一些其他内容的调整和重构</p></li><ul class=\" list-paddingleft-2\" style=\"list-style-type: square;\"><li><p>springframework也从4.3.5.RELEASE升级到5.1.3.RELEASE</p></li><li><p>为减小复杂度service去掉接口和实现类的结构，基础功能的service直接使用实现类；当然具体业务中如果有需求你也可以这没用</p></li><li><p>去掉了一些暂时用不到的maven依赖</p></li><li><p>完善了基础功能的审计功能(之前有介绍审计功能的实现翻番，后续会专门发一篇文档来说明审计功能在系统总的具体用法，当然聪明的你看代码就知道了)</p></li></ul></ul>\r\n                    </div>',1,'1','web-flash 升级 Spring Boot 到 2.1.1 版本'),(4,1,'2019-03-09 16:24:58',1,'2019-04-28 00:34:21','enilu.cn','H5通用官网系统',2,'17','H5通用官网系统'),(5,1,'2019-03-09 16:24:58',1,'2019-04-28 00:34:36','enilu.cn','H5通用论坛系统',2,'18','H5通用论坛系统'),(6,1,'2019-03-09 16:24:58',1,'2019-04-28 00:38:33','enilu.cn','官网建设方案',3,'19','官网建设方案'),(7,1,'2019-03-09 16:24:58',1,'2019-04-28 00:39:48','enilu.cn','论坛建设方案',3,'22','论坛建设方案'),(8,1,'2019-03-09 16:24:58',1,'2019-04-28 17:39:52','enilu.cn','案例1',4,'3','案例1'),(9,1,'2019-03-09 16:24:58',1,'2019-04-28 00:39:11','enilu.cn','案例2',4,'20','案例2'),(14,1,'2019-03-09 16:24:58',1,'2019-04-28 00:39:25','test5','<p>aaaaa<img class=\"wscnph\" src=\"http://127.0.0.1:8082/file/download?idFile=12\" /></p>',4,'21','IDEA的代码生成插件发布啦'),(15,1,'2019-04-28 17:39:52',1,'2019-05-05 15:36:57','enilu','<p><img class=\"wscnph\" src=\"http://127.0.0.1:8082/file/download?idFile=24\" /></p>',1,'25','程序员头冷');
/*!40000 ALTER TABLE `CMSArticle` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `CMSBanner`
--

DROP TABLE IF EXISTS `CMSBanner`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `CMSBanner` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `CreateBy` bigint DEFAULT NULL COMMENT '创建人',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间/注册时间',
  `ModifyBy` bigint DEFAULT NULL COMMENT '最后更新人',
  `ModifyTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  `IdFile` bigint DEFAULT NULL COMMENT 'banner图id',
  `Title` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '标题',
  `Type` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '类型',
  `Url` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '点击banner跳转到url',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8 COMMENT='文章';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CMSBanner`
--

LOCK TABLES `CMSBanner` WRITE;
/*!40000 ALTER TABLE `CMSBanner` DISABLE KEYS */;
INSERT INTO `CMSBanner` VALUES (1,1,'2019-03-09 16:29:03',NULL,NULL,1,'不打开链接','index','javascript:'),(2,1,'2019-03-09 16:29:03',NULL,NULL,2,'打打开站内链接','index','/contact'),(3,1,'2019-03-09 16:29:03',NULL,NULL,6,'打开外部链接','index','http://www.baidu.com'),(4,1,'2019-03-09 16:29:03',NULL,NULL,1,'不打开链接','product','javascript:'),(5,1,'2019-03-09 16:29:03',NULL,NULL,2,'打打开站内链接','product','/contact'),(6,1,'2019-03-09 16:29:03',NULL,NULL,6,'打开外部链接','product','http://www.baidu.com'),(7,1,'2019-03-09 16:29:03',NULL,NULL,1,'不打开链接','solution','javascript:'),(8,1,'2019-03-09 16:29:03',NULL,NULL,2,'打打开站内链接','solution','/contact'),(9,1,'2019-03-09 16:29:03',NULL,NULL,6,'打开外部链接','solution','http://www.baidu.com'),(10,1,'2019-03-09 16:29:03',NULL,NULL,1,'不打开链接','case','javascript:'),(11,1,'2019-03-09 16:29:03',NULL,NULL,2,'打打开站内链接','case','/contact'),(12,1,'2019-03-09 16:29:03',NULL,NULL,6,'打开外部链接','case','http://www.baidu.com'),(14,1,'2019-03-09 16:29:03',NULL,NULL,1,'不打开链接','news','javascript:'),(15,1,'2019-03-09 16:29:03',NULL,NULL,2,'打打开站内链接','news','/contact'),(16,1,'2019-03-09 16:29:03',NULL,NULL,6,'打开外部链接','news','http://www.baidu.com');
/*!40000 ALTER TABLE `CMSBanner` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `CMSChannel`
--

DROP TABLE IF EXISTS `CMSChannel`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `CMSChannel` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `CreateBy` bigint DEFAULT NULL COMMENT '创建人',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间/注册时间',
  `ModifyBy` bigint DEFAULT NULL COMMENT '最后更新人',
  `ModifyTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  `Code` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '编码',
  `Name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '名称',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='文章栏目';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CMSChannel`
--

LOCK TABLES `CMSChannel` WRITE;
/*!40000 ALTER TABLE `CMSChannel` DISABLE KEYS */;
INSERT INTO `CMSChannel` VALUES (1,NULL,NULL,1,'2019-03-13 22:52:46','news','动态资讯'),(2,NULL,NULL,1,'2019-03-13 22:53:11','product','产品服务'),(3,NULL,NULL,1,'2019-03-13 22:53:37','solution','解决方案'),(4,NULL,NULL,1,'2019-03-13 22:53:41','case','精选案例');
/*!40000 ALTER TABLE `CMSChannel` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `CMSContacts`
--

DROP TABLE IF EXISTS `CMSContacts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `CMSContacts` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `CreateBy` bigint DEFAULT NULL COMMENT '创建人',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间/注册时间',
  `ModifyBy` bigint DEFAULT NULL COMMENT '最后更新人',
  `ModifyTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  `Email` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '电子邮箱',
  `Mobile` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '联系电话',
  `Remark` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '备注',
  `UserName` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '邀约人名称',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='邀约信息';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CMSContacts`
--

LOCK TABLES `CMSContacts` WRITE;
/*!40000 ALTER TABLE `CMSContacts` DISABLE KEYS */;
INSERT INTO `CMSContacts` VALUES (1,NULL,'2019-07-31 17:44:27',NULL,'2019-07-31 17:44:27','test@qq.com','15011111111','测试联系，哈哈哈','张三');
/*!40000 ALTER TABLE `CMSContacts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Message`
--

DROP TABLE IF EXISTS `Message`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Message` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `CreateBy` bigint DEFAULT NULL COMMENT '创建人',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间/注册时间',
  `ModifyBy` bigint DEFAULT NULL COMMENT '最后更新人',
  `ModifyTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  `Content` text CHARACTER SET utf8 COLLATE utf8_general_ci COMMENT '消息内容',
  `Receiver` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '接收者',
  `State` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '消息类型,0:初始,1:成功,2:失败',
  `TplCode` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '模板编码',
  `Type` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '消息类型,0:短信,1:邮件',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='历史消息';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Message`
--

LOCK TABLES `Message` WRITE;
/*!40000 ALTER TABLE `Message` DISABLE KEYS */;
INSERT INTO `Message` VALUES (1,NULL,'2019-06-10 21:20:16',NULL,NULL,'【腾讯云】校验码1032，请于5分钟内完成验证，如非本人操作请忽略本短信。','15021592814','2','REGISTER_CODE','0');
/*!40000 ALTER TABLE `Message` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MessageSender`
--

DROP TABLE IF EXISTS `MessageSender`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MessageSender` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `CreateBy` bigint DEFAULT NULL COMMENT '创建人',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间/注册时间',
  `ModifyBy` bigint DEFAULT NULL COMMENT '最后更新人',
  `ModifyTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  `ClassName` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '发送类',
  `Name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '名称',
  `TplCode` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '短信运营商模板编号',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='消息发送者';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageSender`
--

LOCK TABLES `MessageSender` WRITE;
/*!40000 ALTER TABLE `MessageSender` DISABLE KEYS */;
INSERT INTO `MessageSender` VALUES (1,NULL,NULL,NULL,NULL,'tencentSmsSender',' 腾讯短信服务',NULL),(2,NULL,NULL,NULL,NULL,'defaultEmailSender','默认邮件发送器',NULL);
/*!40000 ALTER TABLE `MessageSender` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MessageTemplate`
--

DROP TABLE IF EXISTS `MessageTemplate`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MessageTemplate` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `CreateBy` bigint DEFAULT NULL COMMENT '创建人',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间/注册时间',
  `ModifyBy` bigint DEFAULT NULL COMMENT '最后更新人',
  `ModifyTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  `Code` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '编号',
  `Cond` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '发送条件',
  `Content` text CHARACTER SET utf8 COLLATE utf8_general_ci COMMENT '内容',
  `IDMessageSender` bigint NOT NULL COMMENT '发送者id',
  `Title` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '标题',
  `Type` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '消息类型,0:短信,1:邮件',
  PRIMARY KEY (`ID`) USING BTREE,
  KEY `FK942sadqk5x9kbrwil0psyek3n` (`IDMessageSender`),
  CONSTRAINT `FK942sadqk5x9kbrwil0psyek3n` FOREIGN KEY (`IDMessageSender`) REFERENCES `MessageSender` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COMMENT='消息模板';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageTemplate`
--

LOCK TABLES `MessageTemplate` WRITE;
/*!40000 ALTER TABLE `MessageTemplate` DISABLE KEYS */;
INSERT INTO `MessageTemplate` VALUES (1,NULL,NULL,NULL,NULL,'REGISTER_CODE','注册页面，点击获取验证码','【腾讯云】校验码{1}，请于5分钟内完成验证，如非本人操作请忽略本短信。',1,'注册验证码','0'),(2,NULL,NULL,NULL,NULL,'EMAIL_TEST','测试发送','你好:{1},欢迎使用{2}',2,'测试邮件','1'),(3,NULL,NULL,NULL,NULL,'EMAIL_HTML_TEMPLATE_TEST','测试发送模板邮件','你好<strong>${userName}</strong>欢迎使用<font color=\"red\">${appName}</font>,这是html模板邮件',2,'测试发送模板邮件','1');
/*!40000 ALTER TABLE `MessageTemplate` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SysCfg`
--

DROP TABLE IF EXISTS `SysCfg`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `SysCfg` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `CreateBy` bigint DEFAULT NULL COMMENT '创建人',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间/注册时间',
  `ModifyBy` bigint DEFAULT NULL COMMENT '最后更新人',
  `ModifyTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  `CfgDesc` text CHARACTER SET utf8 COLLATE utf8_general_ci COMMENT '备注',
  `CfgName` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '参数名',
  `CfgValue` varchar(512) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '参数值',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8 COMMENT='系统参数';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SysCfg`
--

LOCK TABLES `SysCfg` WRITE;
/*!40000 ALTER TABLE `SysCfg` DISABLE KEYS */;
INSERT INTO `SysCfg` VALUES (1,NULL,NULL,1,'2019-04-15 21:36:07','应用名称update by 2019-03-27 11:47:04','system.app.name','web-flash'),(2,NULL,NULL,1,'2019-04-15 21:36:17','系统默认上传文件路径','system.file.upload.path','/data/web-flash/runtime/upload'),(3,NULL,NULL,1,'2019-04-15 21:36:17','腾讯sms接口appid','api.tencent.sms.appid','1400219425'),(4,NULL,NULL,1,'2019-04-15 21:36:17','腾讯sms接口appkey','api.tencent.sms.appkey','5f71ed5325f3b292946530a1773e997a'),(5,NULL,NULL,1,'2019-04-15 21:36:17','腾讯sms接口签名参数','api.tencent.sms.sign','需要去申请咯');
/*!40000 ALTER TABLE `SysCfg` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SysDept`
--

DROP TABLE IF EXISTS `SysDept`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `SysDept` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `CreateBy` bigint DEFAULT NULL COMMENT '创建人',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间/注册时间',
  `ModifyBy` bigint DEFAULT NULL COMMENT '最后更新人',
  `ModifyTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  `FullName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Num` int DEFAULT NULL,
  `Pid` bigint DEFAULT NULL,
  `Pids` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `SimpleName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Tips` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Version` int DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8 COMMENT='部门';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SysDept`
--

LOCK TABLES `SysDept` WRITE;
/*!40000 ALTER TABLE `SysDept` DISABLE KEYS */;
INSERT INTO `SysDept` VALUES (24,NULL,NULL,NULL,NULL,'总公司',1,0,'[0],','总公司','',NULL),(25,NULL,NULL,NULL,NULL,'开发部',2,24,'[0],[24],','开发部','',NULL),(26,NULL,NULL,NULL,NULL,'运营部',3,24,'[0],[24],','运营部','',NULL),(27,NULL,NULL,NULL,NULL,'战略部',4,24,'[0],[24],','战略部','',NULL);
/*!40000 ALTER TABLE `SysDept` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SysDict`
--

DROP TABLE IF EXISTS `SysDict`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `SysDict` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `CreateBy` bigint DEFAULT NULL COMMENT '创建人',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间/注册时间',
  `ModifyBy` bigint DEFAULT NULL COMMENT '最后更新人',
  `ModifyTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  `Name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Num` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Pid` bigint DEFAULT NULL,
  `Tips` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=71 DEFAULT CHARSET=utf8 COMMENT='字典';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SysDict`
--

LOCK TABLES `SysDict` WRITE;
/*!40000 ALTER TABLE `SysDict` DISABLE KEYS */;
INSERT INTO `SysDict` VALUES (16,NULL,NULL,NULL,NULL,'状态','0',0,NULL),(17,NULL,NULL,NULL,NULL,'启用','1',16,NULL),(18,NULL,NULL,NULL,NULL,'禁用','2',16,NULL),(29,NULL,NULL,NULL,NULL,'性别','0',0,NULL),(30,NULL,NULL,NULL,NULL,'男','1',29,NULL),(31,NULL,NULL,NULL,NULL,'女','2',29,NULL),(35,NULL,NULL,NULL,NULL,'账号状态','0',0,NULL),(36,NULL,NULL,NULL,NULL,'启用','1',35,NULL),(37,NULL,NULL,NULL,NULL,'冻结','2',35,NULL),(38,NULL,NULL,NULL,NULL,'已删除','3',35,NULL),(53,NULL,NULL,NULL,NULL,'证件类型','0',0,NULL),(54,NULL,NULL,NULL,NULL,'身份证','1',53,NULL),(55,NULL,NULL,NULL,NULL,'护照','2',53,NULL),(68,1,'2019-01-13 14:18:21',1,'2019-01-13 14:18:21','是否','0',0,NULL),(69,1,'2019-01-13 14:18:21',1,'2019-01-13 14:18:21','是','1',68,NULL),(70,1,'2019-01-13 14:18:21',1,'2019-01-13 14:18:21','否','0',68,NULL);
/*!40000 ALTER TABLE `SysDict` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SysFileInfo`
--

DROP TABLE IF EXISTS `SysFileInfo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `SysFileInfo` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `CreateBy` bigint DEFAULT NULL COMMENT '创建人',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间/注册时间',
  `ModifyBy` bigint DEFAULT NULL COMMENT '最后更新人',
  `ModifyTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  `OriginalFileName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `RealFileName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8 COMMENT='文件';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SysFileInfo`
--

LOCK TABLES `SysFileInfo` WRITE;
/*!40000 ALTER TABLE `SysFileInfo` DISABLE KEYS */;
INSERT INTO `SysFileInfo` VALUES (1,1,'2019-03-18 10:34:34',1,'2019-03-18 10:34:34','banner1.png','7e9ebc08-b194-4f85-8997-d97ccb0d2c2d.png'),(2,1,'2019-03-18 10:54:04',1,'2019-03-18 10:54:04','banner2.png','756b9ca8-562f-4bf5-a577-190dcdd25c29.png'),(3,1,'2019-03-18 20:09:59',1,'2019-03-18 20:09:59','offcial_site.png','b0304e2b-0ee3-4966-ac9f-a075b13d4af6.png'),(4,1,'2019-03-18 20:10:18',1,'2019-03-18 20:10:18','bbs.png','67486aa5-500c-4993-87ad-7e1fbc90ac1a.png'),(5,1,'2019-03-18 20:20:14',1,'2019-03-18 20:20:14','product.png','1f2b05e0-403a-41e0-94a2-465f0c986b78.png'),(6,1,'2019-03-18 20:22:09',1,'2019-03-18 20:22:09','profile.jpg','40ead888-14d1-4e9f-abb3-5bfb056a966a.jpg'),(7,1,'2019-03-20 09:05:54',1,'2019-03-20 09:05:54','2303938_1453211.png','87b037da-b517-4007-a66e-ba7cc8cfd6ea.png'),(8,NULL,NULL,NULL,NULL,'login.png','26835cc4-059e-4900-aff5-a41f2ea6a61d.png'),(9,NULL,NULL,NULL,NULL,'login.png','7ec7553b-7c9e-44d9-b9c2-3d89b11cf842.png'),(10,NULL,NULL,NULL,NULL,'login.png','357c4aad-19fd-4600-9fb6-e62aafa3df25.png'),(11,NULL,NULL,NULL,NULL,'index.png','55dd582b-033e-440d-8e8d-c8d39d01f1bb.png'),(12,NULL,NULL,NULL,NULL,'login.png','70507c07-e8bc-492f-9f0a-00bf1c23e329.png'),(13,NULL,NULL,NULL,NULL,'index.png','cd539518-d15a-4cda-a19f-251169f5d1a4.png'),(14,NULL,NULL,NULL,NULL,'login.png','194c8a38-be94-483c-8875-3c62a857ead7.png'),(15,NULL,NULL,NULL,NULL,'index.png','6a6cb215-d0a7-4574-a45e-5fa04dcfdf90.png'),(16,1,'2019-03-21 19:37:50',NULL,NULL,'测试文档.doc','d9d77815-496f-475b-a0f8-1d6dcb86e6ab.doc'),(17,1,'2019-04-28 00:34:09',NULL,NULL,'首页.png','d5aba978-f8af-45c5-b079-673decfbdf26.png'),(18,1,'2019-04-28 00:34:34',NULL,NULL,'资讯.png','7e07520d-5b73-4712-800b-16f88d133db2.png'),(19,1,'2019-04-28 00:38:32',NULL,NULL,'产品服务.png','99214651-8cb8-4488-b572-12c6aa21f30a.png'),(20,1,'2019-04-28 00:39:09',NULL,NULL,'67486aa5-500c-4993-87ad-7e1fbc90ac1a.png','31fdc83e-7688-41f5-b153-b6816d5dfb06.png'),(21,1,'2019-04-28 00:39:22',NULL,NULL,'67486aa5-500c-4993-87ad-7e1fbc90ac1a.png','ffaf0563-3115-477b-b31d-47a4e80a75eb.png'),(22,1,'2019-04-28 00:39:47',NULL,NULL,'7e07520d-5b73-4712-800b-16f88d133db2.png','8928e5d4-933a-4953-9507-f60b78e3ccee.png'),(23,NULL,'2019-04-28 17:34:31',NULL,NULL,'756b9ca8-562f-4bf5-a577-190dcdd25c29.png','7d64ba36-adc4-4982-9ec2-8c68db68861b.png'),(24,NULL,'2019-04-28 17:39:43',NULL,NULL,'timg.jpg','6483eb26-775c-4fe2-81bf-8dd49ac9b6b1.jpg'),(25,1,'2019-05-05 15:36:54',NULL,NULL,'timg.jpg','7fe918a2-c59a-4d17-ad77-f65dd4e163bf.jpg');
/*!40000 ALTER TABLE `SysFileInfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SysLoginLog`
--

DROP TABLE IF EXISTS `SysLoginLog`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `SysLoginLog` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间',
  `IP` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `LoginName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Message` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Succeed` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `UserId` int DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=73 DEFAULT CHARSET=utf8 COMMENT='登录日志';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SysLoginLog`
--

LOCK TABLES `SysLoginLog` WRITE;
/*!40000 ALTER TABLE `SysLoginLog` DISABLE KEYS */;
INSERT INTO `SysLoginLog` VALUES (71,'2019-05-10 13:17:43','127.0.0.1','登录日志',NULL,'成功',1),(72,'2019-05-12 13:36:56','127.0.0.1','登录日志',NULL,'成功',1);
/*!40000 ALTER TABLE `SysLoginLog` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SysMenu`
--

DROP TABLE IF EXISTS `SysMenu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `SysMenu` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `CreateBy` bigint DEFAULT NULL COMMENT '创建人',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间/注册时间',
  `ModifyBy` bigint DEFAULT NULL COMMENT '最后更新人',
  `ModifyTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  `Code` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '编号',
  `Component` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '組件配置',
  `Hidden` bit(1) DEFAULT NULL COMMENT '是否隐藏',
  `Icon` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '图标',
  `IsMenu` bit(1) NOT NULL COMMENT '是否是菜单1:菜单,0:按钮',
  `IsOpen` bit(1) DEFAULT NULL COMMENT '是否默认打开1:是,0:否',
  `Levels` int NOT NULL COMMENT '级别',
  `Name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '名称',
  `Num` int NOT NULL COMMENT '顺序',
  `PCode` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '父菜单编号',
  `PCodes` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '递归父级菜单编号',
  `Status` bit(1) NOT NULL COMMENT '状态1:启用,0:禁用',
  `Tips` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '鼠标悬停提示信息',
  `Url` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '链接',
  PRIMARY KEY (`ID`) USING BTREE,
  UNIQUE KEY `UK_s37unj3gh67ujhk83lqva8i1t` (`Code`)
) ENGINE=InnoDB AUTO_INCREMENT=71 DEFAULT CHARSET=utf8 COMMENT='菜单';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SysMenu`
--

LOCK TABLES `SysMenu` WRITE;
/*!40000 ALTER TABLE `SysMenu` DISABLE KEYS */;
INSERT INTO `SysMenu` VALUES (1,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','system','layout',_binary '\0','system',_binary '',_binary '',1,'系统管理',1,'0','[0],',_binary '',NULL,'/system'),(2,1,'2019-07-31 22:04:30',1,'2019-03-11 22:25:38','cms','layout',_binary '\0','documentation',_binary '',NULL,1,'CMS管理',3,'0','[0],',_binary '',NULL,'/cms'),(3,1,'2019-07-31 22:04:30',1,'2019-06-02 10:09:09','operationMgr','layout',_binary '\0','operation',_binary '',NULL,1,'运维管理',2,'0','[0],',_binary '',NULL,'/optionMgr'),(4,1,'2019-07-31 22:04:30',1,'2019-04-16 18:59:15','mgr','views/system/user/index',_binary '\0','user',_binary '',NULL,2,'用户管理',1,'system','[0],[system],',_binary '',NULL,'/mgr'),(5,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','mgrAdd',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'添加用户',1,'mgr','[0],[system],[mgr],',_binary '',NULL,'/mgr/add'),(6,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','mgrEdit',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'修改用户',2,'mgr','[0],[system],[mgr],',_binary '',NULL,'/mgr/edit'),(7,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','mgrDelete',NULL,_binary '\0',NULL,_binary '\0',_binary '\0',3,'删除用户',3,'mgr','[0],[system],[mgr],',_binary '',NULL,'/mgr/delete'),(8,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','mgrReset',NULL,_binary '\0',NULL,_binary '\0',_binary '\0',3,'重置密码',4,'mgr','[0],[system],[mgr],',_binary '',NULL,'/mgr/reset'),(9,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','mgrFreeze',NULL,_binary '\0',NULL,_binary '\0',_binary '\0',3,'冻结用户',5,'mgr','[0],[system],[mgr],',_binary '',NULL,'/mgr/freeze'),(10,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','mgrUnfreeze',NULL,_binary '\0',NULL,_binary '\0',_binary '\0',3,'解除冻结用户',6,'mgr','[0],[system],[mgr],',_binary '',NULL,'/mgr/unfreeze'),(11,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','mgrSetRole',NULL,_binary '\0',NULL,_binary '\0',_binary '\0',3,'分配角色',7,'mgr','[0],[system],[mgr],',_binary '',NULL,'/mgr/setRole'),(12,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','role','views/system/role/index',_binary '\0','peoples',_binary '',_binary '\0',2,'角色管理',2,'system','[0],[system],',_binary '',NULL,'/role'),(13,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','roleAdd',NULL,_binary '\0',NULL,_binary '\0',_binary '\0',3,'添加角色',1,'role','[0],[system],[role],',_binary '',NULL,'/role/add'),(14,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','roleEdit',NULL,_binary '\0',NULL,_binary '\0',_binary '\0',3,'修改角色',2,'role','[0],[system],[role],',_binary '',NULL,'/role/edit'),(15,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','roleDelete',NULL,_binary '\0',NULL,_binary '\0',_binary '\0',3,'删除角色',3,'role','[0],[system],[role],',_binary '',NULL,'/role/remove'),(16,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','roleSetAuthority',NULL,_binary '\0',NULL,_binary '\0',_binary '\0',3,'配置权限',4,'role','[0],[system],[role],',_binary '',NULL,'/role/setAuthority'),(17,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','menu','views/system/menu/index',_binary '\0','menu',_binary '',_binary '\0',2,'菜单管理',4,'system','[0],[system],',_binary '',NULL,'/menu'),(18,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','menuAdd',NULL,_binary '\0',NULL,_binary '\0',_binary '\0',3,'添加菜单',1,'menu','[0],[system],[menu],',_binary '',NULL,'/menu/add'),(19,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','menuEdit',NULL,_binary '\0',NULL,_binary '\0',_binary '\0',3,'修改菜单',2,'menu','[0],[system],[menu],',_binary '',NULL,'/menu/edit'),(20,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','menuDelete',NULL,_binary '\0',NULL,_binary '\0',_binary '\0',3,'删除菜单',3,'menu','[0],[system],[menu],',_binary '',NULL,'/menu/remove'),(21,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','dept','views/system/dept/index',_binary '\0','dept',_binary '',NULL,2,'部门管理',3,'system','[0],[system],',_binary '',NULL,'/dept'),(22,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','dict','views/system/dict/index',_binary '\0','dict',_binary '',NULL,2,'字典管理',4,'system','[0],[system],',_binary '',NULL,'/dict'),(23,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','deptEdit',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'修改部门',1,'dept','[0],[system],[dept],',_binary '',NULL,'/dept/update'),(24,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','deptDelete',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'删除部门',1,'dept','[0],[system],[dept],',_binary '',NULL,'/dept/delete'),(25,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','dictAdd',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'添加字典',1,'dict','[0],[system],[dict],',_binary '',NULL,'/dict/add'),(26,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','dictEdit',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'修改字典',1,'dict','[0],[system],[dict],',_binary '',NULL,'/dict/update'),(27,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','dictDelete',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'删除字典',1,'dict','[0],[system],[dict],',_binary '',NULL,'/dict/delete'),(28,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','deptList',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'部门列表',5,'dept','[0],[system],[dept],',_binary '',NULL,'/dept/list'),(29,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','deptDetail',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'部门详情',6,'dept','[0],[system],[dept],',_binary '',NULL,'/dept/detail'),(30,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','dictList',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'字典列表',5,'dict','[0],[system],[dict],',_binary '',NULL,'/dict/list'),(31,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','dictDetail',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'字典详情',6,'dict','[0],[system],[dict],',_binary '',NULL,'/dict/detail'),(32,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','deptAdd',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'添加部门',1,'dept','[0],[system],[dept],',_binary '',NULL,'/dept/add'),(33,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','cfg','views/system/cfg/index',_binary '\0','cfg',_binary '',NULL,2,'参数管理',10,'system','[0],[system],',_binary '',NULL,'/cfg'),(34,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','cfgAdd',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'添加系统参数',1,'cfg','[0],[system],[cfg],',_binary '',NULL,'/cfg/add'),(35,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','cfgEdit',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'修改系统参数',2,'cfg','[0],[system],[cfg],',_binary '',NULL,'/cfg/update'),(36,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','cfgDelete',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'删除系统参数',3,'cfg','[0],[system],[cfg],',_binary '',NULL,'/cfg/delete'),(37,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','task','views/system/task/index',_binary '\0','task',_binary '',NULL,2,'任务管理',11,'system','[0],[system],',_binary '',NULL,'/task'),(38,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','taskAdd',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'添加任务',1,'task','[0],[system],[task],',_binary '',NULL,'/task/add'),(39,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','taskEdit',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'修改任务',2,'task','[0],[system],[task],',_binary '',NULL,'/task/update'),(40,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','taskDelete',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'删除任务',3,'task','[0],[system],[task],',_binary '',NULL,'/task/delete'),(41,1,'2019-03-11 22:29:54',1,'2019-03-11 22:29:54','channel','views/cms/channel/index',_binary '\0','channel',_binary '',NULL,2,'栏目管理',1,'cms','[0],[cms],',_binary '',NULL,'/channel'),(42,1,'2019-03-11 22:30:17',1,'2019-03-11 22:30:17','article','views/cms/article/index',_binary '\0','documentation',_binary '',NULL,2,'文章管理',2,'cms','[0],[cms],',_binary '',NULL,'/article'),(43,1,'2019-03-11 22:30:52',1,'2019-03-11 22:30:52','banner','views/cms/banner/index',_binary '\0','banner',_binary '',NULL,2,'banner管理',3,'cms','[0],[cms],',_binary '',NULL,'/banner'),(44,1,'2019-03-18 19:45:37',1,'2019-03-18 19:45:37','contacts','views/cms/contacts/index',_binary '\0','contacts',_binary '',NULL,2,'邀约管理',4,'cms','[0],[cms],',_binary '',NULL,'/contacts'),(45,1,'2019-03-19 10:25:05',1,'2019-03-19 10:25:05','file','views/cms/file/index',_binary '\0','file',_binary '',NULL,2,'文件管理',5,'cms','[0],[cms],',_binary '',NULL,'/fileMgr'),(46,1,'2019-03-11 22:30:17',1,'2019-03-11 22:30:17','editArticle','views/cms/article/edit.vue',_binary '\0','articleEdit',_binary '',NULL,2,'新建文章',1,'cms','[0],[cms],',_binary '',NULL,'/cms/articleEdit'),(47,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','taskLog','views/system/taskLog/index',_binary '','task',_binary '',NULL,2,'任务日志',4,'system','[0],[system],',_binary '',NULL,'/taskLog'),(48,1,'2019-07-31 22:04:30',1,'2019-06-02 10:25:31','log','views/operation/log/index',_binary '\0','log',_binary '',NULL,2,'业务日志',6,'operationMgr','[0],[operationMgr],',_binary '',NULL,'/log'),(49,1,'2019-07-31 22:04:30',1,'2019-06-02 10:25:36','loginLog','views/operation/loginLog/index',_binary '\0','logininfor',_binary '',NULL,2,'登录日志',6,'operationMgr','[0],[operationMgr],',_binary '',NULL,'/loginLog'),(50,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','logClear',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'清空日志',3,'log','[0],[system],[log],',_binary '',NULL,'/log/delLog'),(51,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','logDetail',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'日志详情',3,'log','[0],[system],[log],',_binary '',NULL,'/log/detail'),(52,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','loginLogClear',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'清空登录日志',1,'loginLog','[0],[system],[loginLog],',_binary '',NULL,'/loginLog/delLoginLog'),(53,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','loginLogList',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'登录日志列表',2,'loginLog','[0],[system],[loginLog],',_binary '',NULL,'/loginLog/list'),(54,1,'2019-06-02 10:10:20',1,'2019-06-02 10:10:20','druid','views/operation/druid/index',_binary '\0','monitor',_binary '',NULL,2,'数据库管理',1,'operationMgr','[0],[operationMgr],',_binary '',NULL,'/druid'),(55,1,'2019-06-02 10:10:20',1,'2019-06-02 10:10:20','swagger','views/operation/api/index',_binary '\0','swagger',_binary '',NULL,2,'接口文档',2,'operationMgr','[0],[operationMgr],',_binary '',NULL,'/swagger'),(56,1,'2019-06-10 21:26:52',1,'2019-06-10 21:26:52','messageMgr','layout',_binary '\0','message',_binary '',NULL,1,'消息管理',4,'0','[0],',_binary '',NULL,'/message'),(57,1,'2019-06-10 21:27:34',1,'2019-06-10 21:27:34','msg','views/message/message/index',_binary '\0','message',_binary '',NULL,2,'历史消息',1,'messageMgr','[0],[messageMgr],',_binary '',NULL,'/history'),(58,1,'2019-06-10 21:27:56',1,'2019-06-10 21:27:56','msgTpl','views/message/template/index',_binary '\0','template',_binary '',NULL,2,'消息模板',2,'messageMgr','[0],[messageMgr],',_binary '',NULL,'/template'),(59,1,'2019-06-10 21:28:21',1,'2019-06-10 21:28:21','msgSender','views/message/sender/index',_binary '\0','sender',_binary '',NULL,2,'消息发送者',3,'messageMgr','[0],[messageMgr],',_binary '',NULL,'/sender'),(60,1,'2019-06-10 21:28:21',1,'2019-06-10 21:28:21','msgClear',NULL,_binary '\0',NULL,_binary '',NULL,2,'清空历史消息',3,'messageMgr','[0],[messageMgr],',_binary '',NULL,NULL),(61,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','msgTplEdit',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'编辑消息模板',1,'msgTpl','[0],[messageMgr],[msgTpl]',_binary '',NULL,NULL),(62,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','msgTplDelete',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'删除消息模板',2,'msgTpl','[0],[messageMgr],[msgTpl]',_binary '',NULL,NULL),(63,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','msgSenderEdit',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'编辑消息发送器',1,'msgSender','[0],[messageMgr],[msgSender]',_binary '',NULL,NULL),(64,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','msgSenderDelete',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'删除消息发送器',2,'msgSender','[0],[messageMgr],[msgSender]',_binary '',NULL,NULL),(65,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','fileUpload',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'上传文件',1,'file','[0],[cms],[file],',_binary '',NULL,NULL),(66,1,'2019-07-31 21:51:33',1,'2019-07-31 21:51:33','bannerEdit',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'编辑banner',1,'banner','[0],[cms],[banner],',_binary '',NULL,NULL),(67,1,'2019-07-31 21:51:33',1,'2019-07-31 21:51:33','bannerDelete',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'删除banner',2,'banner','[0],[cms],[banner],',_binary '',NULL,NULL),(68,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','channelEdit',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'编辑栏目',1,'channel','[0],[cms],[channel],',_binary '',NULL,NULL),(69,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','channelDelete',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'删除栏目',2,'channel','[0],[cms],[channel],',_binary '',NULL,NULL),(70,1,'2019-07-31 22:04:30',1,'2019-07-31 22:04:30','deleteArticle',NULL,_binary '\0',NULL,_binary '\0',NULL,3,'删除文章',2,'article','[0],[cms],[article]',_binary '',NULL,NULL);
/*!40000 ALTER TABLE `SysMenu` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SysNotice`
--

DROP TABLE IF EXISTS `SysNotice`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `SysNotice` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `CreateBy` bigint DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `ModifyBy` bigint DEFAULT NULL,
  `ModifyTime` datetime DEFAULT NULL,
  `Content` varchar(255) DEFAULT NULL,
  `Title` varchar(255) DEFAULT NULL,
  `Type` int DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='通知';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SysNotice`
--

LOCK TABLES `SysNotice` WRITE;
/*!40000 ALTER TABLE `SysNotice` DISABLE KEYS */;
INSERT INTO `SysNotice` VALUES (1,1,'2017-01-11 08:53:20',1,'2019-01-08 23:30:58','欢迎使用web-flash后台管理系统','世界',10);
/*!40000 ALTER TABLE `SysNotice` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SysOperationLog`
--

DROP TABLE IF EXISTS `SysOperationLog`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `SysOperationLog` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `ClassName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `LogName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `LogType` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Message` text CHARACTER SET utf8 COLLATE utf8_general_ci COMMENT '详细信息',
  `Method` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Succeed` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `UserId` int DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='操作日志';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SysOperationLog`
--

LOCK TABLES `SysOperationLog` WRITE;
/*!40000 ALTER TABLE `SysOperationLog` DISABLE KEYS */;
INSERT INTO `SysOperationLog` VALUES (1,'cn.enilu.flash.api.controller.cms.ArticleMgrController','2019-05-10 13:22:49','添加参数','业务日志','参数名称=system.app.name','upload','成功',1),(2,'cn.enilu.flash.api.controller.cms.ArticleMgrController','2019-06-10 13:31:09','修改参数','业务日志','参数名称=system.app.name','upload','成功',1),(3,'cn.enilu.flash.api.controller.cms.ArticleMgrController','2019-07-10 13:22:49','编辑文章','业务日志','参数名称=system.app.name','upload','成功',1),(4,'cn.enilu.flash.api.controller.cms.ArticleMgrController','2019-08-10 13:31:09','编辑栏目','业务日志','参数名称=system.app.name','upload','成功',1);
/*!40000 ALTER TABLE `SysOperationLog` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SysRelation`
--

DROP TABLE IF EXISTS `SysRelation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `SysRelation` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `MenuId` bigint DEFAULT NULL,
  `RoleId` bigint DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=144 DEFAULT CHARSET=utf8 COMMENT='菜单角色关系';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SysRelation`
--

LOCK TABLES `SysRelation` WRITE;
/*!40000 ALTER TABLE `SysRelation` DISABLE KEYS */;
INSERT INTO `SysRelation` VALUES (1,42,1),(2,70,1),(3,46,1),(4,43,1),(5,67,1),(6,66,1),(7,33,1),(8,34,1),(9,36,1),(10,35,1),(11,41,1),(12,69,1),(13,68,1),(14,2,1),(15,44,1),(16,21,1),(17,32,1),(18,24,1),(19,29,1),(20,23,1),(21,28,1),(22,22,1),(23,25,1),(24,27,1),(25,31,1),(26,26,1),(27,30,1),(28,54,1),(29,45,1),(30,65,1),(31,48,1),(32,50,1),(33,51,1),(34,49,1),(35,52,1),(36,53,1),(37,17,1),(38,18,1),(39,20,1),(40,19,1),(41,56,1),(42,4,1),(43,5,1),(44,7,1),(45,6,1),(46,9,1),(47,8,1),(48,11,1),(49,10,1),(50,57,1),(51,60,1),(52,59,1),(53,64,1),(54,63,1),(55,58,1),(56,62,1),(57,61,1),(58,3,1),(59,12,1),(60,13,1),(61,15,1),(62,14,1),(63,16,1),(64,55,1),(65,1,1),(66,37,1),(67,38,1),(68,40,1),(69,39,1),(70,47,1),(128,41,2),(129,42,2),(130,43,2),(131,44,2),(132,45,2),(133,46,2),(134,65,2),(135,66,2),(136,67,2),(137,68,2),(138,69,2),(139,70,2),(143,2,2);
/*!40000 ALTER TABLE `SysRelation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SysRole`
--

DROP TABLE IF EXISTS `SysRole`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `SysRole` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `CreateBy` bigint DEFAULT NULL COMMENT '创建人',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间/注册时间',
  `ModifyBy` bigint DEFAULT NULL COMMENT '最后更新人',
  `ModifyTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  `DeptId` bigint DEFAULT NULL,
  `Name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Num` int DEFAULT NULL,
  `Pid` bigint DEFAULT NULL,
  `Tips` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Version` int DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='角色';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SysRole`
--

LOCK TABLES `SysRole` WRITE;
/*!40000 ALTER TABLE `SysRole` DISABLE KEYS */;
INSERT INTO `SysRole` VALUES (1,NULL,NULL,NULL,NULL,24,'超级管理员',1,0,'administrator',1),(2,NULL,NULL,NULL,NULL,25,'网站管理员',1,1,'developer',NULL);
/*!40000 ALTER TABLE `SysRole` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SysTask`
--

DROP TABLE IF EXISTS `SysTask`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `SysTask` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `CreateBy` bigint DEFAULT NULL COMMENT '创建人',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间/注册时间',
  `ModifyBy` bigint DEFAULT NULL COMMENT '最后更新人',
  `ModifyTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  `Concurrent` bit(1) DEFAULT NULL COMMENT '是否允许并发',
  `Cron` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '定时规则',
  `Data` text CHARACTER SET utf8 COLLATE utf8_general_ci COMMENT '执行参数',
  `Disabled` bit(1) DEFAULT NULL COMMENT '是否禁用',
  `ExecAt` datetime DEFAULT NULL COMMENT '执行时间',
  `ExecResult` text CHARACTER SET utf8 COLLATE utf8_general_ci COMMENT '执行结果',
  `JobClass` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '执行类',
  `JobGroup` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '任务组名',
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '任务名',
  `Note` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '任务说明',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8 COMMENT='定时任务';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SysTask`
--

LOCK TABLES `SysTask` WRITE;
/*!40000 ALTER TABLE `SysTask` DISABLE KEYS */;
INSERT INTO `SysTask` VALUES (1,1,'2018-12-28 09:54:00',-1,'2019-03-27 11:47:11',_binary '\0','0 0/30 * * * ?','{\n\"appname\": \"web-flash\",\n\"version\":1\n}\n            \n            \n            \n            \n            \n            \n            \n            \n            \n            \n            \n            ',_binary '\0','2019-03-27 11:47:00','执行成功','cn.enilu.flash.service.task.job.HelloJob','default','测试任务','测试任务,每30分钟执行一次');
/*!40000 ALTER TABLE `SysTask` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SysTaskLog`
--

DROP TABLE IF EXISTS `SysTaskLog`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `SysTaskLog` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `ExecAt` datetime DEFAULT NULL COMMENT '执行时间',
  `ExecSuccess` bit(1) DEFAULT NULL COMMENT '执行结果（成功:1、失败:0)',
  `IdTask` bigint DEFAULT NULL,
  `JobException` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '抛出异常',
  `Name` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '任务名',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='定时任务日志';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SysTaskLog`
--

LOCK TABLES `SysTaskLog` WRITE;
/*!40000 ALTER TABLE `SysTaskLog` DISABLE KEYS */;
/*!40000 ALTER TABLE `SysTaskLog` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SysUser`
--

DROP TABLE IF EXISTS `SysUser`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `SysUser` (
  `ID` bigint NOT NULL AUTO_INCREMENT,
  `CreateBy` bigint DEFAULT NULL COMMENT '创建人',
  `CreateTime` datetime DEFAULT NULL COMMENT '创建时间/注册时间',
  `ModifyBy` bigint DEFAULT NULL COMMENT '最后更新人',
  `ModifyTime` datetime DEFAULT NULL COMMENT '最后更新时间',
  `Account` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '账户',
  `Avatar` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Birthday` datetime DEFAULT NULL,
  `DeptId` bigint DEFAULT NULL,
  `Email` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT 'email',
  `Name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '姓名',
  `Password` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '密码',
  `Phone` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '手机号',
  `RoleId` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '角色id列表，以逗号分隔',
  `Salt` varchar(16) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL COMMENT '密码盐',
  `Sex` int DEFAULT NULL,
  `Status` bit(1) DEFAULT NULL,
  `Version` int DEFAULT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='账号';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SysUser`
--

LOCK TABLES `SysUser` WRITE;
/*!40000 ALTER TABLE `SysUser` DISABLE KEYS */;
INSERT INTO `SysUser` VALUES (-1,NULL,NULL,NULL,NULL,'system',NULL,NULL,NULL,NULL,'应用系统',NULL,NULL,NULL,NULL,NULL,NULL,NULL),(1,NULL,'2016-01-29 08:49:53',1,'2019-03-20 23:45:24','admin',NULL,'2017-05-05 00:00:00',27,'eniluzt@qq.com','管理员','b5a51391f271f062867e5984e2fcffee','15021222222','1','8pgby',2,_binary '',25),(2,NULL,'2018-09-13 17:21:02',1,'2019-01-09 23:05:51','developer',NULL,'2017-12-31 00:00:00',25,'eniluzt@qq.com','网站管理员','fac36d5616fe9ebd460691264b28ee27','15022222222','2,','vscp9',1,_binary '',NULL);
/*!40000 ALTER TABLE `SysUser` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-02-20 19:09:14
