﻿using MvcGrid.Models;

namespace MvcGrid.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMvcGridTemplatingEngine
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        string Process(string template, TemplateModel model);
    }
}