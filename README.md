# mitoSoft.TimeSwitches.Core
A .Net 5.0 library to handle daily timers in .Net projects where you can set one trigger time a days.
If you want more than one triggering time per day use more than one **'DailyTimer'**.

It includes an interface for a time-switch model as well as helpers to check the status of the timer.
Whenever a timer exceeds its switching time, the **'Checker'** class is able to regognize this by it **'CheckState'** function.

Additionally, it is possible to deactivate the timer in holidays.

## Dependencies

 - mitoSoft.Holidays (Version 1.1.3)

## Example usage

```c#
  var timer = new Timer();

  var checker = new Checker(timer, Holidays.Provinces.RheinlandPfalz);

  while (true)
  {
    if (checker.CheckState())
    {
      //this routine will be triggered every day at 12 o'clock
      //do some stuff here
      //...
    }
  }
  
  //...
```

with the example Model:

```c#
  class Timer : IDailyTimer
  {
    public bool Active { get; set; } = true;
    public string Name { get; set; } = "Test";
    public string Description { get; set; } = "Test";
    public bool Monday { get; set; } = true;
    public bool Tuesday { get; set; } = true;
    public bool Wednesday { get; set; } = true;
    public bool Thursday { get; set; } = true;
    public bool Friday { get; set; } = true;
    public bool Saturday { get; set; } = true;
    public bool Sunday { get; set; } = true;
    public string SwitchTime { get; set; } = "12:00";
    public bool IgnoreOnHolidays { get; set; } = false;

    public object Clone()
    {
      throw new System.NotImplementedException();
    }
  }
```
