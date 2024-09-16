/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright.Core;
using Microsoft.Playwright.Transport;

using Xunit;
using Moq;
using AutoShopWeb.Components.Pages;

namespace AutoShopTDDSuite
{
    public class PlaywrightTests
    {
        [Fact]
        public void OnInitialized_SetsCurrentPageCorrectly()
        {
            // Arrange
            var navManager = new MockNavigationManager();
            var component = new Home();
            component.navManager = navManager;

            // Act
            component.OnInitialized();

            // Assert
            Assert.Equal("/", component.currentpage); // Assuming default URI is "/home"
        }

        [Fact]
        public void IsSelected_ReturnsTrueForMatchingUri()
        {
            // Arrange
            var navManager = new MockNavigationManager();
            navManager.SetUri("http://example.com/services");
            var component = new YourComponent();
            component.navManager = navManager;

            // Act
            var isSelected = component.IsSelected("/services");

            // Assert
            Assert.True(isSelected);
        }

        [Fact]
        public void IsSelected_ReturnsFalseForNonMatchingUri()
        {
            // Arrange
            var navManager = new MockNavigationManager();
            navManager.SetUri("http://example.com/home");
            var component = new YourComponent();
            component.navManager = navManager;

            // Act
            var isSelected = component.IsSelected("/services");

            // Assert
            Assert.False(isSelected);
        }


    }
}
*/