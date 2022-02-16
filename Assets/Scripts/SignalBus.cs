using System;
using System.Collections.Generic;

public class SignalBus
{
    public static SignalBus Instance
    {
        get
        {
            if (_instance == null)
                _instance = new SignalBus();

            return _instance;
        }
    }
    
    public void Subscribe<TSignal>(Action callback)
    {
        void WrapperCallback(object o) => callback();
        SubscribeInternal(typeof(TSignal), callback, WrapperCallback);
    }

    public void Subscribe<TSignal>(Action<TSignal> callback)
    {
        void WrapperCallback(object o) => callback((TSignal) o);
        SubscribeInternal(typeof(TSignal), callback, WrapperCallback);
    }

    public void Unsubscribe<TSignal>(Action callback)
    {
        UnsubscribeInternal(typeof(TSignal), callback);
    }

    public void Unsubscribe<TSignal>(Action<TSignal> callback)
    {
        UnsubscribeInternal(typeof(TSignal), callback);
    }

    public void Fire<TSignal>()
    {
        FireInternal(typeof(TSignal), Activator.CreateInstance<TSignal>());
    }

    public void Fire(object signal)
    {
        FireInternal(signal.GetType(), signal);
    }
    
    private static SignalBus _instance;

    private Dictionary<Type, SignalDeclaration> _subscriptions = new Dictionary<Type, SignalDeclaration>();

    private SignalBus()
    {
        
    }

    private void SubscribeInternal(Type type, object token, Action<object> callback)
    {
        if (!_subscriptions.ContainsKey(type))
            _subscriptions.Add(type, new SignalDeclaration());

        _subscriptions[type].Subscribe(token, callback);
    }

    private void UnsubscribeInternal(Type type, object token)
    {
        if (!_subscriptions.ContainsKey(type))
            return;

        _subscriptions[type].Unsubscribe(token);
    }

    private void FireInternal(Type type, object signal)
    {
        if (!_subscriptions.ContainsKey(type))
            throw new Exception($"No subscription found for signal type of {nameof(type)}");
        
        _subscriptions[type].Fire(signal);
    }

    private class SignalDeclaration
    {
        private Dictionary<object, Action<object>> _subscriptions =
            new Dictionary<object, Action<object>>();

        public void Subscribe(object token, Action<object> callback)
        {
            _subscriptions.Add(token, callback);
        }

        public void Unsubscribe(object token)
        {
            _subscriptions.Remove(token);
        }

        public void Fire(object signal)
        {
            foreach (var subscription in _subscriptions)
                subscription.Value.Invoke(signal);
        }
    }
}
