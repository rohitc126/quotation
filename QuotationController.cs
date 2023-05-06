using BusinessLayer.DAL;
using BusinessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eSGBIZ.Controllers
{
    public class QuotationController : BaseController
    {

        public ActionResult QuotationPreparation()
        {
            Quation_Preparation Entry = new Quation_Preparation();
            ViewBag.Header = "Quotation Entry";

            List<Product_Master> prodList = new DAL_Common().GetProductnameList();
            Entry.Product_List = new SelectList(prodList, "productCode", "productName");

            List<CustomerMaster> custList = new DAL_Common().GetCustomerMaster_List("", "");
            Entry.CUSTOMER_LIST = new SelectList(custList, "custCode", "custName");

          

            //List<ProductGrade_Master> GradeList = new DAL_Common().GetGrade("");
            //Entry.Grade_List = new SelectList(GradeList, "ProductGrade_Code", "Grade_Name");

            //List<PRODUCT_SIZE_MST> sizeList = new DAL_Common().GetProductSize("");
            //Entry.PRODUCT_SIZ_LIST = new SelectList(sizeList, "ProductSize_Code", "Size");

         

            return View(Entry);

        }

        [HttpPost]
        public ActionResult QuotationPreparation(Quation_Preparation Entry)
        {
            return RedirectToAction("QuotationPreparationEntry", Entry);
           
        }



        public ActionResult QuotationPreparationEntry(Quation_Preparation Entry)
        {
            
            VM_Quotation_Entry obj = new VM_Quotation_Entry();


            try
            {
                obj.CUST_CODE = Entry.CUST_CODE;
                List<CustomerMaster> custList = new DAL_Common().GetCustomerMaster_List("", "");
                obj.CUST_NAME = custList.Where(x => x.custCode == obj.CUST_CODE).FirstOrDefault().custName;

                //obj.PACKAGE_ID = Entry.PACKAGE_ID;
                //List<Packaging_Master> packageList = new DAL_Common().GetPackageList();
                //obj.PACKAGE_TYPE_NAME = custList.Where(x => x.PACKAGE_ID == obj.PACKAGE_ID).FirstOrDefault().PACKAGE_TYPE_NAME;

                //Product details
                Product_Master prod = new DAL_Common().GetProductnameList().Where(x=>x.productCode==Entry.productCode).FirstOrDefault();

                ProductGrade_Master Grade = new DAL_Common().GetGrade(Entry.productCode).Where(x => x.ProductGrade_Code == Entry.ProductGrade_Code).FirstOrDefault();

                ProductType_Master type = new DAL_Common().GetType(Entry.productCode).Where(x => x.ProductType_Code == Entry.ProductType_Code).FirstOrDefault();

                PRODUCT_SIZE_MST size = new DAL_Common().GetProductSize(Entry.ProductType_Code).Where(x => x.ProductSize_Code == Entry.ProductSize_Code).FirstOrDefault();



           
               
                List<VM_Product_DATA> ProductList = new List<VM_Product_DATA>();

            
                ProductList.Add(new VM_Product_DATA {  SrNo = 1 , 
                                                        productCode = Entry.productCode,
                                                       ProductType_Code = Entry.ProductType_Code,
                                                       ProductGrade_Code = Entry.ProductGrade_Code,
                                                       ProductSize_Code = Entry.ProductSize_Code,
                                                       productName = prod.productName,
                                                       productDesc = prod.productDesc
                                                    });
                obj.Product_Dtl = ProductList;

                List<Packaging_Master> packageList = new DAL_Common().GetPackageList();
                obj.Packaging_List = new SelectList(packageList,"PACKAGE_ID", "PACKAGE_TYPE_NAME");

            }
            catch (Exception ex)
            {
                //logger.Error(ex, "Error : _CNs_Data_List ", ex.Message);
                Danger(string.Format("<b>Exception occured.</b>"), true);
            }


            return View(obj);
        }

        //public ActionResult QuotationPackage(VM_Quotation_Entry Entry)
        //{
         
             
        //        List<Packaging_Master> packageList = new DAL_Common().GetPackageList();
        //        Entry.Packaging_List = new SelectList("PACKAGE_ID", "PACKAGE_TYPE_NAME");

        //        return View(packageList);
        //}


        //public ActionResult QuotationPackageEntry(VM_Quotation_Entry Entry)
        //{

        //    VM_Product_DATA obj = new VM_Product_DATA();


        //    try
        //    {


        //        obj.PACKAGE_ID = Entry.PACKAGE_ID;
        //        List<Packaging_Master> packageList = new DAL_Common().GetPackageList();
        //        obj.PACKAGE_TYPE_NAME = packageList.Where(x => x.package_id == obj.PACKAGE_ID).FirstOrDefault().package_type_name;
                
        //        //ProductGrade_Master Grade = new DAL_Common().GetGrade(Entry.productCode).Where(x => x.ProductGrade_Code == Entry.ProductGrade_Code).FirstOrDefault();
        //        Packaging_Master Package = new DAL_Common().GetPackageList().Where(x => x.package_id == Entry.PACKAGE_ID).FirstOrDefault();



        //        List<VM_Product_DATA> ProductList = new List<VM_Product_DATA>();


        //        ProductList.Add(new VM_Product_DATA
        //        {
        //            SrNo = 1,
        //            PACKAGE_ID = Entry.PACKAGE_ID,
                 
        //        });
        //      //  obj.Product_Dtl = ProductList;


        //    }
        //    catch (Exception ex)
        //    {
        //        //logger.Error(ex, "Error : _CNs_Data_List ", ex.Message);
        //        Danger(string.Format("<b>Exception occured.</b>"), true);
        //    }


        //    return View(obj);
        //}


        } 

    }

   