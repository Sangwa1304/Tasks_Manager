using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryTcpApi.Models;

/// <summary>
/// Models pour la deserialisation des 
/// </summary>
/// <param name="Name"></param>
/// <param name="Parameters"></param>
public record ResquestModel(string Name, object Parameters);

