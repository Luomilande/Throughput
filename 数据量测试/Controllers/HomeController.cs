using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using 数据量测试.Models;

namespace 数据量测试.Controllers
{
    public class HomeController : Controller
    {
        private Models.BookEntities1 db = new Models.BookEntities1();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public string Spit1()
        {
            //EF：简单数据-2446毫秒
            Stopwatch watch1 = Stopwatch.StartNew();
            List<student> st_data = db.student.ToList();
            if (st_data.Count==0) return "读取失败";
            watch1.Stop();

            return "EF最后用时："+watch1.ElapsedMilliseconds;
        }
        public string Spit2()
        {
            //存储过程：简单数据-572毫秒
            Stopwatch watch2 = Stopwatch.StartNew();
            List<student> st_data2 = db.Database.SqlQuery<student>("exec sp_Select_Student").ToList();
            if (st_data2.Count == 0) return "读取失败";
            watch2.Stop();

            return "存储过程用时：" + watch2.ElapsedMilliseconds;
        }
        public string Test()
        {
           
            List<Models.student> List = new List< Models.student > ();

            for (int number = 0; number < 50000; number++)
            {
                string name = GetTestData();
                bool i_sex = true;

                 Random r = new Random();
                 int val = r.Next(1, 9);
                 if (val % 2 == 0) i_sex = false;
                 List.Add(new Models.student { s_name=name,sex= i_sex});
            }

            //存储过程
            //db.sp_Insert_Student(number, name,sex);
            Stopwatch watch = Stopwatch.StartNew();
            //使用EntityFramework.BulkInsert插件进行大批量数据存储
            db.BulkInsert(List);
            db.BulkSaveChanges();
            watch.Stop();

            return "ok,用时："+watch.ElapsedMilliseconds+"数据量为:"+List.Count;
        }


        /// <summary>
        /// 姓氏列表
        /// </summary>
        string[] arrLastName = null;
        /// <summary>
        /// 生成的名字列表
        /// </summary>
        List<string> listCnNames = new List<string>();
        /// <summary>
        /// 名字字符的个数
        /// </summary>
        Random rdCharCount = new Random();
        /// <summary>
        /// 姓氏在姓氏列表中的索引
        /// </summary>
        Random rdLastNameIndex = new Random();
        /// <summary>
        /// 名字的字符对应的十进制
        /// </summary>
        Random rdFirstName = new Random();

        /// <summary>
        /// 随机姓名
        /// </summary>
        /// <returns></returns>
        public string GetTestData()
        {
            arrLastName = strLastName.Replace(" ", " ")
              .Replace("\t", " ").Split(new char[] { ' ' });

            string name = "";
            //姓
            name += arrLastName[rdLastNameIndex.Next(0, arrLastName.Length - 1)];
            //名
            int iCharCount = rdCharCount.Next(1, 2);
            for (int iCharCountIndex = 1; iCharCountIndex <= iCharCount; iCharCountIndex++)
            {
                name += (char)rdFirstName.Next(16128, 36597);
            }
            return name;
        }

        string strLastName = "赵 钱 孙 李    周 吴 郑 王    冯    陈    褚    卫    蒋    沈    韩    杨    朱    秦    尤    许 "
+ "何 吕    施    张    孔    曹    严    华    金    魏    陶    姜    戚    谢    邹    喻    柏    水    窦    章 "
+ "云 苏    潘    葛    奚    范    彭    郎    鲁    韦    昌    马    苗    凤    花    方    俞    任    袁    柳 "
+ "酆 鲍    史    唐    费    廉    岑    薛    雷    贺    倪    汤    滕    殷    罗    毕    郝    邬    安    常 "
+ "乐 于    时    傅    皮    卞    齐    康    伍    余    元    卜    顾    孟    平    黄    和    穆    萧    尹 "
+ "姚 邵    湛    汪    祁    毛    禹    狄    米    贝    明    臧    计    伏    成    戴    谈    宋    茅    庞 "
+ "熊 纪    舒    屈    项    祝    董    粱    杜    阮    蓝    闵    席    季    麻    强    贾    路    娄    危 "
+ "江 童    颜    郭    梅    盛    林    刁    钟    徐    邱    骆    高    夏    蔡    田    樊    胡    凌    霍 "
+ "虞 万    支    柯    昝    管    卢    莫    经    房    裘    缪    干    解    应    宗    丁    宣    贲    邓 "
+ "郁 单    杭    洪    包    诸    左    石    崔    吉    钮    龚    程    嵇    邢    滑    裴    陆    荣    翁 "
+ "荀 羊    於    惠    甄    麴    家    封    芮    羿    储    靳    汲    邴    糜    松    井    段    富    巫 "
+ "乌 焦    巴    弓    牧    隗    山    谷    车    侯    宓    蓬    全    郗    班    仰    秋    仲    伊    宫 "
+ "宁 仇    栾    暴    甘    钭    厉    戎    祖    武    符    刘    景    詹    束    龙    叶    幸    司    韶 "
+ "郜    黎    蓟    薄    印    宿    白    怀    蒲    邰    从    鄂    索    咸    籍    赖    卓    蔺    屠    蒙 "
+ "池    乔    阴    欎    胥    能    苍    双    闻    莘    党    翟    谭    贡    劳    逄    姬    申    扶    堵 "
+ "冉    宰    郦    雍    舄    璩    桑    桂    濮    牛    寿    通    边    扈    燕    冀    郏    浦    尚    农 "
+ "温    别    庄    晏    柴    瞿    阎    充    慕    连    茹    习    宦    艾    鱼    容    向    古    易    慎 "
+ "戈    廖    庾    终    暨    居    衡    步    都    耿    满    弘    匡    国    文    寇    广    禄    阙    东 "
+ "殴    殳    沃    利    蔚    越    夔    隆    师    巩    厍    聂    晁    勾    敖    融    冷    訾    辛    阚 "
+ "那    简    饶    空    曾    毋    沙    乜    养    鞠    须    丰    巢    关    蒯    相    查    後    荆    红 "
+ "游    竺    权    逯    盖    益    桓    公    万俟    司马    上官    欧阳    夏侯    诸葛 "
+ "闻人    东方    赫连    皇甫    尉迟    公羊    澹台    公冶    宗政    濮阳 "
+ "淳于    单于    太叔    申屠    公孙    仲孙    轩辕    令狐    钟离    宇文 "
+ "长孙    慕容    鲜于    闾丘    司徒    司空    亓官    司寇    仉    督    子车 "
+ "颛孙    端木    巫马    公西    漆雕    乐正    壤驷    公良    拓跋    夹谷 "
+ "宰父    谷梁    晋    楚    闫    法    汝    鄢    涂    钦    段干    百里    东郭    南门 "
+ "呼延    归    海    羊舌    微生    岳    帅    缑    亢    况    后    有    琴    梁丘    左丘 "
+ "东门    西门    商    牟    佘    佴    伯    赏    南宫    墨    哈    谯    笪    年    爱    阳    佟 "
+ "第五    言    福";
    }
}
