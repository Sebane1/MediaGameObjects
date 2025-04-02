using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.Types;
using InteropGenerator.Runtime;
using System;
using System.Numerics;
using IMediaGameObject = RoleplayingMediaCore.IMediaGameObject;

namespace RoleplayingVoiceDalamudWrapper
{
    public unsafe class MediaGameObject : IMediaGameObject
    {
        private Dalamud.Game.ClientState.Objects.Types.IGameObject _gameObject;
        private nint _address;
        private FFXIVClientStructs.FFXIV.Client.Game.Object.GameObject* _gameObjectPointer;
        private string _name = "";
        private Vector3 _position = new Vector3();

        string IMediaGameObject.Name
        {
            get
            {
                try
                {
                    return _gameObjectPointer != null ? (_gameObjectPointer != null ? _gameObjectPointer->NameString : _name) : _name;
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
                    return (_gameObjectPointer != null ? _gameObjectPointer->Position : _position);
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
                    return new Vector3(0, _gameObjectPointer != null ? _gameObjectPointer->Rotation : 0, 0);
                }
                catch
                {
                    return new Vector3(0, 0, 0);
                }
            }
        }

        // To do: re-implement target detection
        //string IMediaGameObject.FocusedPlayerObject {
        //    get {
        //        if (_gameObject != null) {
        //            try {
        //                return _gameObject.TargetObject != null ?
        //                    (_gameObject.TargetObject.ObjectKind == ObjectKind.Player ? _gameObject.TargetObject.Name.TextValue : "")
        //                    : "";
        //            } catch {
        //                return "";
        //            }
        //        } else {
        //            return "";
        //        }
        //    }
        //}

        string IMediaGameObject.FocusedPlayerObject
        {
            get
            {
                return "";
            }
        }

        Vector3 IMediaGameObject.Forward
        {
            get
            {
                float rotation = _gameObjectPointer != null ? _gameObjectPointer->Rotation : 0;
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

        bool IMediaGameObject.Invalid => false;

        public nint Address { get => _address; set => _address = value; }

        public MediaGameObject(nint address)
        {
            _address = address;
            _gameObjectPointer = (FFXIVClientStructs.FFXIV.Client.Game.Object.GameObject*)address;
        }
        unsafe public MediaGameObject(FFXIVClientStructs.FFXIV.Client.Game.Object.GameObject* gameObject)
        {
            _gameObjectPointer = gameObject;
        }
        public MediaGameObject(string name, Vector3 position)
        {
            _name = name;
            _position = position;
        }
        public MediaGameObject(nint address, string name, Vector3 position)
        {
            _address = address;
            _gameObjectPointer = (FFXIVClientStructs.FFXIV.Client.Game.Object.GameObject*)address;
            _name = name;
            _position = position;
        }
    }
}
