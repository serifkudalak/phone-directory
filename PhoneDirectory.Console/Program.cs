using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneDirectory.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string firstName, lastName, phone;
            int reply;
            bool result;
            UserManager userManager = new UserManager();

            back:
            System.Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz :)\n" +
                                     "*******************************************\n" +
                                     "(1) Yeni Numara Kaydetmek\n" +
                                     "(2) Varolan Numarayı Silmek\n" +
                                     "(3) Varolan Numarayı Güncelleme\n" + 
                                     "(4) Rehberi Listelemek\n" +
                                     "(5) Rehberde Arama Yapmak"﻿);

            System.Console.Write("Seçim: ");
            reply = Convert.ToInt32(System.Console.ReadLine());

            switch (reply)
            {
                case 1:
                    System.Console.WriteLine();
                    System.Console.WriteLine("Kayıt Ekleme");
                    System.Console.Write("İsim giriniz: ");
                    firstName = System.Console.ReadLine();
                    System.Console.Write("Soyisim giriniz: ");
                    lastName = System.Console.ReadLine();
                    System.Console.Write("Telefon giriniz: ");
                    phone = System.Console.ReadLine();

                    userManager.Add(new User
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Phone = phone
                    });
                    goto back;

                case 2:
                    remove:
                    System.Console.WriteLine();
                    System.Console.WriteLine("Kayıt Silme (İsim ve Soyisim Aynı Anda Girmek Zorunlu Değil)");
                    System.Console.Write("İsim giriniz: ");
                    firstName = System.Console.ReadLine();
                    System.Console.Write("Soyisim giriniz: ");
                    lastName = System.Console.ReadLine();
                    result = userManager.Delete(new User
                    {
                        FirstName = firstName,
                        LastName = lastName
                    });

                    if (result == false)
                    {
                        System.Console.WriteLine("* Silmeyi sonlandırmak için : (1)\n" +
                                                 "* Yeniden denemek için: (2)");

                        System.Console.Write("Seç: ");
                        reply = Convert.ToInt32(System.Console.ReadLine());

                        if (reply == 1)
                        {
                            goto back;
                        }
                        else
                        {
                            goto remove;
                        }
                    }

                    goto back;

                case 3:
                    update:
                    System.Console.WriteLine();
                    System.Console.WriteLine("Kayıt Güncelleme");
                    System.Console.Write("Güncellenecek kişinin telefon numarasını giriniz: ");
                    phone = System.Console.ReadLine();
                    User userDetail = userManager.GetByPhoneNumber(phone);

                    if (userDetail != null)
                    {
                        System.Console.Write("İsim giriniz: ");
                        firstName = System.Console.ReadLine();
                        System.Console.Write("Soyisim giriniz: ");
                        lastName = System.Console.ReadLine();
                        System.Console.Write("Telefon giriniz: ");
                        phone = System.Console.ReadLine();
                        result = userManager.Update(userDetail, new User { FirstName = firstName, LastName = lastName, Phone = phone });
                        if (result == false)
                        {
                            System.Console.WriteLine("* Güncellemeyi sonlandırmak için : (1)\n" +
                                                     "* Yeniden denemek için: (2)");

                            System.Console.Write("Seç: ");
                            reply = Convert.ToInt32(System.Console.ReadLine());

                            if (reply == 1)
                            {
                                goto back;
                            }
                            else
                            {
                                goto update;
                            }
                        }
                    }
                    else
                    {
                        goto back;
                    }


                    goto back;

                case 4:
                    System.Console.WriteLine();
                    System.Console.WriteLine("Telefon Rehberi");
                    System.Console.WriteLine("********************");
                    foreach (var item in userManager.Get())
                    {
                        System.Console.WriteLine($"İsim: {item.FirstName}\nSoyisim: {item.LastName}\nTelefon Numarası: {item.Phone}\n-");
                    }

                    goto back;

                case 5:
                    System.Console.WriteLine();
                    System.Console.WriteLine("Arama yapmak istediğiniz tipi seçiniz.\n" +
                                             "**********************************************\n\n" +
                                             "İsim veya soyisime göre arama yapmak için: (1)\n" +
                                             "Telefon numarasına göre arama yapmak için: (2)");
                    System.Console.Write("Seç: ");
                    reply = Convert.ToInt32(System.Console.ReadLine());

                    if (reply == 1)
                    {
                        System.Console.Write("İsim giriniz: ");
                        firstName = System.Console.ReadLine();
                        System.Console.Write("Soyisim giriniz: ");
                        lastName = System.Console.ReadLine();
                        User userInformation = userManager.GetByFirstOrLastName(firstName, lastName);

                        if (userInformation != null)
                        {
                            System.Console.WriteLine($"İsim: {userInformation.FirstName}\nSoyisim: {userInformation.LastName}\nTelefon: {userInformation.Phone}");
                            goto back;
                        }
                        else
                        {
                            goto back;
                        }
                    }
                    else if(reply == 2)
                    {
                        System.Console.Write("Telefon giriniz: ");
                        phone = System.Console.ReadLine();
                        User userInformation = userManager.GetByPhoneNumber(phone);

                        if (userInformation != null)
                        {
                            System.Console.WriteLine($"İsim: {userInformation.FirstName}\nSoyisim: {userInformation.LastName}\nTelefon: {userInformation.Phone}");
                            goto back;
                        }
                        else
                        {
                            goto back;
                        }
                    }
                    else
                    {
                        goto back;
                    }           
                default:
                    break;
            }

            Environment.Exit(0);
        }
    }

    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
    }

    public class UserManager
    {
        List<User> _users;

        public UserManager()
        {
            _users = new List<User>
            {
                new User { FirstName = "Şerif", LastName = "KUDALAK", Phone = "111 111 11 11" },
                new User { FirstName = "Umut", LastName = "TOPKAYA", Phone = "222 222 22 22" },
                new User { FirstName = "Abdullah", LastName = "ORMANCI", Phone = "333 333 33 33" },
                new User { FirstName = "Eyüp", LastName = "DİK", Phone = "444 444 44 44" },
                new User { FirstName = "Salih", LastName = "CEYLAN", Phone = "555 555 55 55" }
            };
        }

        public List<User> Get()
        {
            return _users;
        }

        public User GetByPhoneNumber(string phone)
        {
            try
            {
                if (String.IsNullOrEmpty(phone))
                    throw new Exception("Telefon Numarası Boş Olamaz!");

                var userDetail = _users.Where(_ => _.Phone == phone).FirstOrDefault();

                if (userDetail == null)
                    throw new Exception("Herhangi Bir Kayıt Bulunamadı!");

                return userDetail;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return null;
            }
        }

        public User GetByFirstOrLastName(string firstName, string LastName)
        {
            try
            {
                if (String.IsNullOrEmpty(firstName) && String.IsNullOrEmpty(LastName))
                    throw new Exception("Alanlar Aynı Anda Boş Olamaz!");

                var userDetail = _users.Where(_ => _.FirstName == firstName || _.LastName == LastName).FirstOrDefault();

                if (userDetail == null)
                    throw new Exception("Herhangi Bir Kayıt Bulunamadı!");

                return userDetail;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void Add(User user)
        {
            try
            {
                if (String.IsNullOrEmpty(user.FirstName) || String.IsNullOrEmpty(user.LastName) || String.IsNullOrEmpty(user.Phone))
                    throw new Exception("Alanlar Boş Olamaz!");

                _users.Add(user);
                System.Console.WriteLine($"{user.FirstName + " " + user.LastName} isimli kişi rehbere eklendi.");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        public bool Delete(User user)
        {
            bool result = false;

            try
            {
                if (String.IsNullOrEmpty(user.FirstName) && String.IsNullOrEmpty(user.LastName))
                    throw new Exception("Alanların İkisi Birden Boş Olamaz!");

                var userDetail = _users.Find(_ => _.FirstName == user.FirstName || _.LastName == user.LastName);

                if (userDetail == null)
                    throw new Exception("Aradığınız krtiterlere uygun veri rehberde bulunamadı. Lütfen bir seçim yapınız.");

                char reply;
                System.Console.Write($"{userDetail.FirstName + " " + userDetail.LastName} isimli kişi rehberden silinmek üzere, onaylıyor musunuz ?(y/n)");
                reply = Convert.ToChar(System.Console.ReadLine());

                if (reply == 'y' || reply == 'Y')
                {
                    _users.Remove(userDetail);
                    System.Console.WriteLine($"{userDetail.FirstName + " " + userDetail.LastName} isimli kişi rehberden silindi.");
                    result = true;
                }
                else
                {
                    System.Console.WriteLine("Kayıt Silme Vazgeçildi");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return result;
        }

        public bool Update(User user,User userUpdate)
        {
            bool result = false;

            try
            {
                if (String.IsNullOrEmpty(userUpdate.FirstName) || String.IsNullOrEmpty(userUpdate.LastName) || String.IsNullOrEmpty(userUpdate.Phone))
                    throw new Exception("Alanlar Boş Olamaz!");

                char reply;
                System.Console.Write($"{user.FirstName + " " + user.LastName} isimli kişi rehberde güncellenmek üzere, onaylıyor musunuz ?(y/n)");
                reply = Convert.ToChar(System.Console.ReadLine());

                if (reply == 'y' || reply == 'Y')
                {
                    User tempUser = new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Phone = user.Phone
                    };

                    user.FirstName = userUpdate.FirstName;
                    user.LastName = userUpdate.LastName;
                    user.Phone = userUpdate.Phone;
                    System.Console.WriteLine($"{tempUser.FirstName + " " + tempUser.LastName} isimli kişinin kaydı {userUpdate.FirstName + " " + userUpdate.LastName} olarak güncellendi.");
                    result = true;
                }
                else
                {
                    System.Console.WriteLine("Kayıt Silme Vazgeçildi");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
