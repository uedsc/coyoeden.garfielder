/*
Javascript Module Pattern 模板v1.0
Author:Levin Van
Last Modified On 2010.05.25
此模板用于js客户端开发,发布时别忘压缩js以便去掉模板中的备注
*/
var Garfielder = (function ($) {
    var p = {}, pub = {};
    /*private area*/
    p._modules = {};
    /*
    initVar方法
    作用：用于引用重复使用的dom元素或引用服务器端生成到页面的js变量
    */
    p.initVar = function (opts) {
        pub.SiteRoot = opts.siteRoot;
    };
    /*
    onLoaded方法
    作用:统一管理页面加载完毕后的回调方法
    说明:onLoaded方法接管所有页面上注册到$(document).ready(callback)中的callback方法;
    如果你要新增一个$(callback)或$(document).ready,请将你的callback方法放在onLoaded方法体内
    */
    p.onLoaded = function () {
        for (var m in p._modules) {
            if ((m = p._modules[m]) && m.onLoad) {
                m.onLoad(m);
            };
        };
    };
    /*
    initEvents方法
    作用:用于为页面dom元素注册各种事件!
    说明:Html页面仅用于表现，任何时候应在标签里面直接注册事件。即避免如<a onclick="xx"/>
    */
    p.initEvents = function (opts) {
        $(document).ready(p.onLoaded);
    };
    /*/private area*/
    /*public area*/
    /* 调用Init之前,子模块可以设置Cfg将子模块的私有配置注入到Garfield.js中 */
    pub.Cfg = null;
    /*
    Init方法
    作用:页面js逻辑的唯一入口
    说明：理想状态下每个页面对应一个交互用的js文件，在页面末尾通过下面代码初始化js交互逻辑
    <script type="text/javascript">
    //<![CDATA[
    this$.Init({x:'kk',y:'zz'});
    //]]>
    </script> 
    */
    pub.Init = function (opts) {
        if (Garfielder.Cfg) {
            Garfielder.Cfg = $.extend(opts, Garfielder.Cfg);
        } else {
            Garfielder.Cfg = opts;
        };
        p.initVar(Garfielder.Cfg);
        //init modules
        for (var m in p._modules) {
            if ((m = p._modules[m]) && m.init) {
                m.init(Garfielder.Cfg);
            };
        };
        //init events
        p.initEvents(Garfielder.Cfg);
    };
    /**
    * 往当前页面JS逻辑注册一个功能模块.请在Init方法前调用
    * @param {Object} key
    * @param {Object} module 如'{init:function(opts){},onLoad:function(opts){}}'
    */
    pub.AddModule = function (key, module) {
        if (p._modules[key]) {
            alert("Module with key '" + key + "' has beed registered!");
            return;
        };
        p._modules[key] = module;
        return pub;
    };
    /**
    * 获取指定注册的模块
    * @param {Object} key
    */
    pub.GetModule = function (key) {
        return p._modules[key];
    };
    /**
    * Shortcut for GetModule and AddModule!
    * @param {String} module key
    * @param {module} module instance
    */
    pub.M = function (key, module) {
        if (arguments.length === 1) {
            return pub.GetModule(key);
        };
        if (arguments.length === 2) {
            return pub.AddModule(key,module);
        };
        return null;
    };
    /**
    * 获取指定长度的随机字符串。注意：仅仅由数字和字母组成
    * @param {Object} size 随机字符串的长度
    * @param {Boolean} plusTimeStamp 是否加上当前时间戳
    */
    pub.RdStr = function (size, plusTimeStamp) {
        var size0 = 8;
        var chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz";
        size = size || size0; size = size < 1 ? size0 : size; size = size > chars.length ? size0 : size;
        var s = '';
        for (var i = 0; i < size; i++) {
            var rnum = Math.floor(Math.random() * chars.length);
            s += chars.substring(rnum, rnum + 1);
        };
        if (plusTimeStamp) {
            s += new Date().getTime();
        };
        return s;
    };
    /**
    * 执行指定html模板
    *
    */
    pub.EvalTpl = function (str, data) {
        ///<summary>
        ///simple js template parser
        ///E.G,IF:str="<a href=/u/%uid%>%username%</a>"
        ///data={uid:1,username:'xiami'}
        ///Then:str = "<a href=/u/1>xiami</a>"
        ///</summary>
        var result;
        var patt = new RegExp("%([a-zA-z0-9]+)%");
        while ((result = patt.exec(str)) != null) {
            var v = data[result[1]] || '';
            str = str.replace(new RegExp(result[0], "g"), v);
        };
        return str;
    };
    /**
    * 截断字符串
    * @param {String} str		 待截断的字符串
    * @param {int} size		   截断长,注:1个中文字符长度为2
    * @param {String} tailStr	截断后加在末尾的小尾巴,默认"..."
    */
    pub.Tail = function (str, size, tailStr) {
        str = StringUtils.trim(str);
        var cLen = StringUtils.charLength(str);
        size = size <= 0 ? cLen : size;
        if (size >= cLen) return str;
        while (StringUtils.charLength(str) > size) {
            str = str.substr(0, str.length - 1);
        }
        str += (tailStr || "...");
        return str;
    };
    /*/public area*/
    return pub;
})(jQuery); 