﻿using BookShop.Models.ViewModels;
using Nazar1988.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.Repository
{
    public class BooksRepository
    {
        private readonly NazarDbContext _context;

        public BooksRepository(NazarDbContext context)
        {
            _context = context;
        }
        public List<TreeViewCategory> GetAllCategories()
        {
            var Categories = (from c in _context.Categories
                              where (c.ParentCategoryID == null)
                              select new TreeViewCategory { CategoryID = c.CategoryID, CategoryName = c.CategoryName }).ToList();
            foreach (var item in Categories)
            {
                BindSubCategories(item);
            }

            return Categories;
        }
        public void BindSubCategories(TreeViewCategory category)
        {
            var SubCategories = (from c in _context.Categories
                                 where (c.ParentCategoryID == category.CategoryID)
                                 select new TreeViewCategory { CategoryID = c.CategoryID, CategoryName = c.CategoryName }).ToList();
            foreach(var item in SubCategories)
            {
                BindSubCategories(item);
                category.SubCategories.Add(item);
            }
        }
    }
}
