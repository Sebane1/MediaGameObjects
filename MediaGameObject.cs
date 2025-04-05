using Dalamud.Game.ClientState.Objects.Enums;
using DragAndDropTexturing.ThreadSafeDalamudObjectTable;
using System;
using System.Numerics;
using IMediaGameObject = RoleplayingMediaCore.IMediaGameObject;

namespace RoleplayingVoiceDalamudWrapper
{
    public unsafe class MediaGameObject : IMediaGameObject
    {
        ThreadSafeGameObject _threadSafeGameObject;
        private string _name = "";
        private Vector3 _position = new Vector3();

        string IMediaGameObject.Name
        {
            get
            {
                try
                {
                    return (_threadSafeGameObject != null ? _threadSafeGameObject.Name.TextValue : _name);
                }
                catch
                {
                    return _name;
                }
            }
        }

        Vector3 IMediaGameObject.Position
        {
            get
            {
                try
                {
                    return (_threadSafeGameObject != null ? _threadSafeGameObject.Position : _position);
                }
                catch
                {
                    return _position;
                }
            }
        }

        Vector3 IMediaGameObject.Rotation
        {
            get
            {
                try
                {
                    return new Vector3(0, _threadSafeGameObject != null ? _threadSafeGameObject.Rotation : 0, 0);
                }
                catch
                {
                    return new Vector3(0, 0, 0);
                }
            }
        }

        string IMediaGameObject.FocusedPlayerObject
        {
            get
            {
                if (_threadSafeGameObject != null)
                {
                    try
                    {
                        return _threadSafeGameObject.TargetObject != null ?
                            (_threadSafeGameObject.TargetObject.ObjectKind == ObjectKind.Player ? _threadSafeGameObject.TargetObject.Name.TextValue : "")
                            : "";
                    }
                    catch
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
        }
        Vector3 IMediaGameObject.Forward
        {
            get
            {
                float rotation = _threadSafeGameObject != null ? _threadSafeGameObject.Rotation : 0;
                return new Vector3((float)Math.Cos(rotation), 0, (float)Math.Sin(rotation));
            }
        }

        public Vector3 Top
        {
            get
            {
                return new Vector3(0, 1, 0);
            }
        }

        public Dalamud.Game.ClientState.Objects.Types.IGameObject GameObject { get => _threadSafeGameObject; }

        bool IMediaGameObject.Invalid => false;

        public MediaGameObject(ThreadSafeGameObject threadSafeGameObject)
        {
            _threadSafeGameObject = threadSafeGameObject;
        }

        public MediaGameObject(string name, Vector3 position)
        {
            _name = name;
            _position = position;
        }
        public MediaGameObject(ThreadSafeGameObject threadSafeGameObject, string name, Vector3 position)
        {
            _threadSafeGameObject = threadSafeGameObject;
            _name = name;
            _position = position;
        }
    }
}
