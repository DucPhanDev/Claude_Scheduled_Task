using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventManager
{
    // Dictionary lưu multicast delegate theo loại event
    private static readonly Dictionary<Type, Delegate> _handlers = new Dictionary<Type, Delegate>();

    /// <summary>
    /// Đăng ký lắng nghe một event kiểu T
    /// </summary>
    public static void Subscribe<T>(Action<T> handler) where T : class, IEvent
    {
        if (handler == null) return;

        Type eventType = typeof(T);

        if (_handlers.TryGetValue(eventType, out var existing))
        {
            _handlers[eventType] = Delegate.Combine(existing, handler);
        }
        else
        {
            _handlers[eventType] = handler;
        }
    }

    /// <summary>
    /// Hủy đăng ký lắng nghe event kiểu T
    /// Nên gọi trong OnDisable() của MonoBehaviour để tránh memory leak
    /// </summary>
    public static void Unsubscribe<T>(Action<T> handler) where T : class, IEvent
    {
        Type eventType = typeof(T);

        if (!_handlers.TryGetValue(eventType, out var existing)) return;

        var newDelegate = Delegate.Remove(existing, handler);

        if (newDelegate == null)
        {
            _handlers.Remove(eventType);
        }
        else
        {
            _handlers[eventType] = newDelegate;
        }
    }

    /// <summary>
    /// Phát ra event, gọi tất cả handler đã đăng ký
    /// </summary>
    public static void Raise<T>(T eventData) where T : class, IEvent
    {
        Type eventType = typeof(T);

        if (_handlers.TryGetValue(eventType, out var del))
        {
            try
            {
                // Gọi multicast delegate trực tiếp
                (del as Action<T>)?.Invoke(eventData);
            }
            catch (Exception ex)
            {
                Debug.LogError($"[EventManager] Lỗi khi raise {eventType.Name}: {ex.Message}\n{ex.StackTrace}");
            }
        }
    }

    /// <summary>
    /// Xóa hết handler của một loại event cụ thể
    /// </summary>
    public static void Clear<T>() where T : class, IEvent
    {
        _handlers.Remove(typeof(T));
    }

    /// <summary>
    /// Xóa toàn bộ handler (dùng khi reset game, unload scene...)
    /// </summary>
    public static void ClearAll()
    {
        _handlers.Clear();
    }
}
