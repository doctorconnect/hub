using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doctorhubBusinessEntities
{
   public class UploadDocument :Base
    {
          public int ID  {get;set;} 
          public string Message {get;set;} 
          public string Title  {get;set;} 
          public string Category  {get;set;} 
          public string Name  {get;set;} 
          public string UploadBy  {get;set;} 
          public string UserCode      {get;set;}   
          public string UploadByName  {get;set;} 
          public int Status  {get;set;} 
          public string type {get;set;} 
          public string Remarks  {get;set;} 
          public string UploadDate  {get;set;}
    }
}
