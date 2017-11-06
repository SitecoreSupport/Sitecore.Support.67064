using Sitecore.Forms.Mvc.TypeConverters;
using Sitecore.Forms.Mvc.ViewModels;
using Sitecore.WFFM.Abstractions.Actions;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Sitecore.Support.Forms.Mvc.ViewModels.Fields
{
  public class DatePickerField : ValuedFieldViewModel<string>
  {
    public override string Value
    {
      get
      {
        return base.Value;
      }
      set
      {
        base.Value = (DateUtil.IsIsoDate(value) ? DateUtil.IsoDateToDateTime(value).ToString(this.DateFormat ?? "yy-MM-dd", new CultureInfo(Sitecore.Context.Language.CultureInfo.TwoLetterISOLanguageName)) : value);
      }
    }

    public string DateFormat
    {
      get;
      set;
    }

    [TypeConverter(typeof(IsoDateTimeConverter))]
    public DateTime StartDate
    {
      get;
      set;
    }

    [TypeConverter(typeof(IsoDateTimeConverter))]
    public DateTime EndDate
    {
      get;
      set;
    }

    public DatePickerField()
    {
      this.DateFormat = "yy-MM-dd";
      this.StartDate = DateUtil.IsoDateToDateTime("20000101T120000");
      this.EndDate = DateTime.Now.AddYears(1).Date;
    }

    public override void Initialize()
    {
      if (string.IsNullOrEmpty(this.Value))
      {
        this.Value = DateTime.Now.ToString(this.DateFormat, new CultureInfo(Sitecore.Context.Language.CultureInfo.TwoLetterISOLanguageName));
      }
    }

    public override ControlResult GetResult()
    {
      return new ControlResult(base.FieldItemId, this.Title, DateUtil.ToIsoDate(DateTime.ParseExact(this.Value, this.DateFormat, new CultureInfo(Sitecore.Context.Language.CultureInfo.TwoLetterISOLanguageName))), this.ResultParameters, false);
    }
  }
}