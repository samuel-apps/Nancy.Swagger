﻿using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;
using Nancy.Swagger.Demo.Models;
using Nancy.Swagger.Annotations.Attributes;
using Swagger.ObjectModel;

namespace Nancy.Swagger.Demo.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get("/", _ => Response.AsRedirect("/swagger-ui/dist/index.html"), null, "Home");

            Get("/users", _ => GetUsers(), null, "GetUsers");

            Post("/users", _ =>
            {
                var user = this.Bind<User>();
                return PostUser(user);
            });
        }

        [Route("GetUsers")]
        [Route(HttpMethod.Get, "/users")]
        [Route(Summary = "Get All Users")]
        [SwaggerResponse(HttpStatusCode.OK, Message = "OK", Model = typeof(IEnumerable<User>))]
        [Route(Tags = new[] { "Users" })]
        private IEnumerable<User> GetUsers()
        {
            return new[] {new User {Name = "Vincent Vega", Age = 45}};
        }

        [Route(HttpMethod.Post, "/users")]
        [Route(Summary = "Post New User")]
        [SwaggerResponse(HttpStatusCode.OK, Message = "OK", Model = typeof(User))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Message = "Internal Server Error")]
        [Route(Produces = new[] { "application/json" })]
        [Route(Consumes = new[] { "application/json", "application/xml" })]
        [Route(Tags = new[] { "Users" })]
        private User PostUser([RouteParam(ParameterIn.Body)] User user)
        {
            return user;
        } 
    }
}