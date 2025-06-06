using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Adaptive.Intelligence.SecureApi.Server;

/// <summary>
/// Represents a security failure result of an HTTP operation.
/// </summary>
/// <seealso cref="IActionResult" />
public class SecurityErrorResult : IActionResult
{
    #region Constructors 
    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityErrorResult"/> class.
    /// </summary>
    public SecurityErrorResult()
    {

    }
    /// <summary>
    /// Initializes a new instance of the <see cref="SecurityErrorResult"/> class.
    /// </summary>
    /// <param name="message">
    /// A string containing the error message.
    /// </param>
    public SecurityErrorResult(string message)
    {
        Message = message;
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the message text.
    /// </summary>
    /// <value>
    /// A string containing the error message.
    /// </value>
    public string Message { get; set; } = string.Empty;
    #endregion

    #region Public Methods    
    /// <summary>
    /// Executes the result operation of the action method asynchronously. This method is called by MVC to process
    /// the result of an action method.
    /// </summary>
    /// <param name="context">The context in which the result is executed. The context information includes
    /// information about the action that was executed and request information.</param>
    /// <returns>
    /// A task that represents the asynchronous execute operation.
    /// </returns>
    public Task ExecuteResultAsync(ActionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
        context.HttpContext.Response.WriteAsync(Message);
        return Task.CompletedTask;
    }
    #endregion
}
