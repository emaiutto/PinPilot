using System;
using System.Runtime.CompilerServices;

namespace FSUIPC;

public sealed class FsLVar
{
    private double _value;
    public int ID { get; set; }
    public string Name { get; }
    public bool ValueChanged { get; private set; }
    public double Value => _value;

    public event EventHandler<LVarEvent>? OnValueChanged;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetValue(double newValue)
    {
        // check if es entero sin usar Math.Round
        bool isInt = newValue == Math.Floor(newValue);

        if (isInt)
        {
            if ((uint)newValue <= 65535)
            {
                WAPI.fsuipcw_setLvarAsUShort((ushort)ID, (ushort)newValue);
                return;
            }
            if (newValue >= -32768 && newValue <= 32767)
            {
                WAPI.fsuipcw_setLvarAsShort((ushort)ID, (short)newValue);
                return;
            }
        }

        WAPI.fsuipcw_setLvarAsDouble((ushort)ID, newValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void UpdateValue(double newValue)
    {
        if (newValue != _value)
        {
            _value = newValue;
            ValueChanged = true;

            var handler = OnValueChanged;
            if (handler != null)
                handler(this, new LVarEvent(this));
        }
        else
        {
            ValueChanged = false;
        }
    }

    internal FsLVar(int id, string name)
    {
        ID = id;
        Name = name;
    }

}
