﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WarCardGame.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IUser", CallbackContract=typeof(WarCardGame.ServiceReference1.IUserCallback))]
    public interface IUser {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUser/Join", ReplyAction="http://tempuri.org/IUser/JoinResponse")]
        bool Join(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUser/Join", ReplyAction="http://tempuri.org/IUser/JoinResponse")]
        System.Threading.Tasks.Task<bool> JoinAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUser/CanBePlayed", ReplyAction="http://tempuri.org/IUser/CanBePlayedResponse")]
        bool CanBePlayed(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUser/CanBePlayed", ReplyAction="http://tempuri.org/IUser/CanBePlayedResponse")]
        System.Threading.Tasks.Task<bool> CanBePlayedAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUser/addCard", ReplyAction="http://tempuri.org/IUser/addCardResponse")]
        string addCard(string name, WarCardGameLibrary.Card card);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUser/addCard", ReplyAction="http://tempuri.org/IUser/addCardResponse")]
        System.Threading.Tasks.Task<string> addCardAsync(string name, WarCardGameLibrary.Card card);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IUser/Leave")]
        void Leave(string name);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IUser/Leave")]
        System.Threading.Tasks.Task LeaveAsync(string name);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IUser/PostMessage")]
        void PostMessage(string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IUser/PostMessage")]
        System.Threading.Tasks.Task PostMessageAsync(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUser/GetAllMessages", ReplyAction="http://tempuri.org/IUser/GetAllMessagesResponse")]
        string[] GetAllMessages();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUser/GetAllMessages", ReplyAction="http://tempuri.org/IUser/GetAllMessagesResponse")]
        System.Threading.Tasks.Task<string[]> GetAllMessagesAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IUser/SendAllMessages")]
        void SendAllMessages(string[] messages);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserChannel : WarCardGame.ServiceReference1.IUser, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserClient : System.ServiceModel.DuplexClientBase<WarCardGame.ServiceReference1.IUser>, WarCardGame.ServiceReference1.IUser {
        
        public UserClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public UserClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public UserClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public UserClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public UserClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public bool Join(string name) {
            return base.Channel.Join(name);
        }
        
        public System.Threading.Tasks.Task<bool> JoinAsync(string name) {
            return base.Channel.JoinAsync(name);
        }
        
        public bool CanBePlayed(string name) {
            return base.Channel.CanBePlayed(name);
        }
        
        public System.Threading.Tasks.Task<bool> CanBePlayedAsync(string name) {
            return base.Channel.CanBePlayedAsync(name);
        }
        
        public string addCard(string name, WarCardGameLibrary.Card card) {
            return base.Channel.addCard(name, card);
        }
        
        public System.Threading.Tasks.Task<string> addCardAsync(string name, WarCardGameLibrary.Card card) {
            return base.Channel.addCardAsync(name, card);
        }
        
        public void Leave(string name) {
            base.Channel.Leave(name);
        }
        
        public System.Threading.Tasks.Task LeaveAsync(string name) {
            return base.Channel.LeaveAsync(name);
        }
        
        public void PostMessage(string message) {
            base.Channel.PostMessage(message);
        }
        
        public System.Threading.Tasks.Task PostMessageAsync(string message) {
            return base.Channel.PostMessageAsync(message);
        }
        
        public string[] GetAllMessages() {
            return base.Channel.GetAllMessages();
        }
        
        public System.Threading.Tasks.Task<string[]> GetAllMessagesAsync() {
            return base.Channel.GetAllMessagesAsync();
        }
    }
}
