// Decompiled with JetBrains decompiler
// Type: AstralBattles.ServiceReference.IGameServiceChannel
// Assembly: AstralBattles, Version=1.4.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 0ADAD7A2-9432-4E3E-A56A-475E988D1430
// Assembly location: C:\Users\Admin\Desktop\RE\Astral_Battles_v1.4\AstralBattles.dll

using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.ServiceModel.Channels;

#nullable disable
namespace AstralBattles.ServiceReference
{
  [GeneratedCode("System.ServiceModel", "4.0.0.0")]
  public interface IGameServiceChannel : 
    IGameService,
    IClientChannel,
    IContextChannel,
    IChannel,
    ICommunicationObject,
    IExtensibleObject<IContextChannel>,
    IDisposable
  {
  }
}
