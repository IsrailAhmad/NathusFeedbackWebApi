﻿using EverGreenWebApi.DBHelper;
using EverGreenWebApi.Interfaces;
using EverGreenWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EverGreenWebApi.Repository
{
    public class MenuSetupRepository : IMenuSetupRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MenuSetUpMasterModel> SetupMenu(MenuSetupModel model)
        {
            ResponseStatus response = new ResponseStatus();
            List<MenuSetUpMasterModel> resultdata = new List<MenuSetUpMasterModel>();
            using (nathusfeedback_androidEntities context = new nathusfeedback_androidEntities())
            {
                var data = context.menusetupmasters.Where(w => w.menuid == model.MenuId && w.categoryid == model.CategoryId && w.Productid == model.ProductId).FirstOrDefault();
                if (data != null)
                {

                }
                else
                {
                    menusetupmaster m = new menusetupmaster();
                    m.menuid = model.MenuId;
                    m.categoryid = model.CategoryId;
                    m.Productid = model.ProductId;
                    context.menusetupmasters.Add(m);
                    var result = context.SaveChanges();
                    if (result > 0)
                    {
                        resultdata = (from ms in context.menusetupmasters
                                      join me in context.menumasters on ms.menuid equals me.MenuId into j1
                                      from j2 in j1.DefaultIfEmpty()
                                      join cat in context.categorymasters on ms.categoryid equals cat.CategoryId into j3
                                      from j4 in j3.DefaultIfEmpty()
                                      join pro in context.productmasters on ms.Productid equals pro.ProductId into j5
                                      from j6 in j5.DefaultIfEmpty()
                                      orderby ms.createdon descending
                                      select new MenuSetUpMasterModel()
                                      {
                                          id = ms.id,
                                          MenuName = j2.MenuName,
                                          CategoryName = j4.CategoryName,
                                          ProductName = j6.ProductName,
                                          CreatedOn = (DateTime)ms.createdon
                                      }).ToList();
                    }
                }
            }
            return resultdata;
        }

        public IEnumerable<MenuSetUpMasterModel> GetAllMenuList(int menuid)
        {
            ResponseStatus response = new ResponseStatus();
            List<MenuSetUpMasterModel> resultdata = new List<MenuSetUpMasterModel>();
            using (nathusfeedback_androidEntities context = new nathusfeedback_androidEntities())
            {
                if (menuid == 0)
                {
                    resultdata = (from ms in context.menusetupmasters
                                  join me in context.menumasters on ms.menuid equals me.MenuId into j1
                                  from j2 in j1.DefaultIfEmpty()
                                  join cat in context.categorymasters on ms.categoryid equals cat.CategoryId into j3
                                  from j4 in j3.DefaultIfEmpty()
                                  join pro in context.productmasters on ms.Productid equals pro.ProductId into j5
                                  from j6 in j5.DefaultIfEmpty()
                                  //where ms.menuid == menuid
                                  orderby ms.createdon descending
                                  select new MenuSetUpMasterModel()
                                  {
                                      id = ms.id,
                                      MenuName = j2.MenuName,
                                      CategoryName = j4.CategoryName,
                                      ProductName = j6.ProductName,
                                      CreatedOn = (DateTime)ms.createdon
                                  }).ToList();
                } else
                {

                    resultdata = (from ms in context.menusetupmasters
                                  join me in context.menumasters on ms.menuid equals me.MenuId into j1
                                  from j2 in j1.DefaultIfEmpty()
                                  join cat in context.categorymasters on ms.categoryid equals cat.CategoryId into j3
                                  from j4 in j3.DefaultIfEmpty()
                                  join pro in context.productmasters on ms.Productid equals pro.ProductId into j5
                                  from j6 in j5.DefaultIfEmpty()
                                  where ms.menuid == menuid
                                  orderby ms.createdon descending
                                  select new MenuSetUpMasterModel()
                                  {
                                      id = ms.id,
                                      MenuName = j2.MenuName,
                                      CategoryName = j4.CategoryName,
                                      ProductName = j6.ProductName,
                                      CreatedOn = (DateTime)ms.createdon
                                  }).ToList();
                }
            }
            return resultdata;
        }

        public IEnumerable<MenuSetUpMasterModel> RemoveSetupMenu(int id)
        {
            ResponseStatus response = new ResponseStatus();
            List<MenuSetUpMasterModel> resultdata = new List<MenuSetUpMasterModel>();
            using (nathusfeedback_androidEntities context = new nathusfeedback_androidEntities())
            {
                context.menusetupmasters.Remove(context.menusetupmasters.Where(d => d.id == id).First());
                var result = context.SaveChanges();
                if (result > 0)
                {
                    resultdata = (from ms in context.menusetupmasters
                                  join me in context.menumasters on ms.menuid equals me.MenuId into j1
                                  from j2 in j1.DefaultIfEmpty()
                                  join cat in context.categorymasters on ms.categoryid equals cat.CategoryId into j3
                                  from j4 in j3.DefaultIfEmpty()
                                  join pro in context.productmasters on ms.Productid equals pro.ProductId into j5
                                  from j6 in j5.DefaultIfEmpty()
                                  orderby ms.createdon descending
                                  select new MenuSetUpMasterModel()
                                  {
                                      id = ms.id,
                                      MenuName = j2.MenuName,
                                      CategoryName = j4.CategoryName,
                                      ProductName = j6.ProductName,
                                      CreatedOn = (DateTime)ms.createdon
                                  }).ToList();
                }

            }
            return resultdata;
        }

    }
}