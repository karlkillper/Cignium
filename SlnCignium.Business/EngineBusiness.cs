using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlnCignium.Entities;
using SlnCignium.Repository;
using System.Text.RegularExpressions;
using System.Web;

namespace SlnCignium.Business
{
    public class EngineBusiness
    {
        EngineRepository obj = new EngineRepository();
        public BeanAnswer QuerySearchEngine(BeanSent snt)
        {
            BeanAnswer ent = new BeanAnswer();
            List<string> lst = new List<string>();
            String searchEncoded = "";
            try
            {
                //lst = getWords(snt.Words);

                searchEncoded = HttpUtility.UrlEncode(snt.Words);
                snt.MainPath = snt.MainPath + searchEncoded;

                ent = obj.QuerySearchEngine(snt);
                String data = string.Join("", ent.Message.ToCharArray().Where(Char.IsDigit));
                ent.Total = Convert.ToInt64(data);
                return ent;
            }
            catch (Exception ex)
            {
                return ent;
            }

        }



        public List<string> getWords(string sentence) {
            List<string> lst = new List<string>();
            sentence = sentence.Trim();
            string xword = "";
            bool flag = false;
            bool f2 = false;
            bool f3 = false;
            int counter = 0;
            int k = 0;
            for (int i = 0; i < 100; i++)
            {
                sentence = sentence.Replace("  "," ");
            }

            if (sentence.Length > 0) {
                char sc = ' ';
                foreach (var item in sentence)
                {
                    sc = item;
                    if (item.Equals(' ') && flag == false)
                    {
                        if (f2 == false && f3 == false)
                        {
                            lst.Add(xword);
                            xword = "";
                            flag = true;
                        }
                        else {
                            xword = xword + Convert.ToString(item);
                            flag = false;
                        }
                    }
                    else {
                        if ( (Convert.ToString(item).Equals("\"") || Convert.ToString(item).Equals("'") ) && flag == true)
                        {
                            if (Convert.ToString(item).Equals("\"") && f2 == false)
                            {

                                foreach (var t in sentence)
                                {
                                    if (k > counter)
                                    {
                                        if (Convert.ToString(t).Equals("\""))
                                        {
                                            f2 = true;
                                        }
                                    }
                                    k++;
                                }
                            }
                            else if (Convert.ToString(item).Equals("\"") && f2 == true)
                            {
                                f2 = false;
                            }
                            else if (Convert.ToString(item).Equals("'") && f3 == false)
                            {

                                foreach (var t in sentence)
                                {
                                    if (k > counter)
                                    {
                                        if (Convert.ToString(t).Equals("\'"))
                                        {
                                            f3 = true;
                                        }
                                    }
                                    k++;
                                }
                            }
                            else if (Convert.ToString(item).Equals("'") && f3 == true)
                            {
                                f3 = false;
                            }
                            else
                            {
                                xword = xword + Convert.ToString(item);
                            }
                        }
                        else
                        {
                            xword = xword + Convert.ToString(item);
                        }

                        if (f2 == true || f3 == true)
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    counter++;
                }//--[ foreach ]




                lst.Add(xword);
            }




            return lst;
        }

    }
}
