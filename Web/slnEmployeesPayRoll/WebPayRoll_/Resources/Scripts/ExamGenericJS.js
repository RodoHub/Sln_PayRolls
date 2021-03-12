/************************************************************************************************************
    Author:          Rodolfo Bernal Sánchez (RBS)
    Date:            March 11, 2021
    Descripción:    Generic utilities for JavaScript
    Lugar:          México, D.F
*************************************************************************************************************/
        //#region "------------------------- Global Initialization for scripts ------------------------

            //Globals
                //Holds main instance
                    var JSObject;

                //Color Transparent
                    var DefaultImagePath_Icons = "/Resources/Images/";

        /// <summary>
        /// Initialize JS Utilities
        /// </summary>
            function InitializeJSs()
            {
                return new RBS_.JSs.ClientSide.JScripter();
            }

            
        //#end region "----------------------- Global Initialization for scripts ----------------------

        //#region "----------------------- Class: RBS.JSs.ClientSideV2.JScripter ----------------------
            var RBS_ =  RBS_ ||
            {
                JSs:
                {
                    ClientSide:
                    { //Begin: ClientSideV2 NameSpace

                                    /// <summary>
                                    /// Contains several generic methods and utilities from client side
                                    /// </summary>
                                        JScripter: function()
                                        {//Begin: JScripter Class 

                                            //#region "------------------------------------------------ AJAX -----------------------------------------------

                                                /// <summary>
                                                /// Execs an action from a controler through Ajax
                                                /// </summary>
                                                /// <param name="url_">Url to consume [ for instance: "/Controller/Action" ]</param>
                                                /// <param name="jsonParams">Json Stringified to send to url</param>
                                                /// <returns></returns>
                                                    this.ExecAjaxRS = function(url_, jsonParams)
                                                    {
                                                        var resultSet;
                                                        $.ajax({
                                                            type: "POST",
                                                            url: url_,
                                                            data: JSON.stringify(jsonParams),
                                                            contentType: "application/json; charset=utf-8",
                                                            dataType: "json",
                                                            async: false,
                                                            success: function (response)
                                                            {
                                                                //Get information from Server
                                                                    resultSet = response;//(typeof response) == 'string' ? eval('(' + response + ')') : response;
                                                            },
                                                            error: function (result)
                                                            {
                                                                this.ShowMessage("ERROR", "Atention", result.status + ' ' + result.statusText);
                                                            }
                                                        });

                                                        return resultSet;
                                                    }

                                            //#endregion "---------------------------------------------- END AJAX ---------------------------------------------

                                            //#region "-------------------------------------------- MESSAGES -------------------------------------------
                            
                                                /// <summary>
                                                /// Shows a message from RBS.Solutions.Web.Components.Filer to User
                                                /// </summary>
                                                /// <returns></returns>
                                                    this.ShowMessage = function(msgType, title ,text)
                                                    {
                                                        var filerImgIcon = DefaultImagePath_Icons;

                                                        switch(msgType)
                                                        {
                                                            case "OK": filerImgIcon += "img_Ok_64x64.png"; break;
                                                            case "ALERT": filerImgIcon += "img_Alert_64x64.png"; break;
                                                            case "ERROR": filerImgIcon += "img_Error_64x64.png"; break;
                                                            case "INFO": filerImgIcon += "img_Info_64x64.png"; break;
                                                        }

                                                        $("div[id$='MainModalPopUpMessageTitle']").text(title);
                                                        $("div[id$='MainModalPopUpMessageText']").html(text);
                                                        $("img[id$='MainModalPopUpMessageImage']").attr("src", filerImgIcon);
                                                        $("#MainModalPopUpMessage").modal('show')

                                                    }

                                                /// <summary>
                                                /// Validate info given
                                                /// </summary>
                                                /// <param name="input_">Current input that fires current method when event 'Lost Focus' is fired</param>
                                                /// <param name="validationNameOrExpression_">[Validation Name|RegularExpression] to apply</param>
                                                /// <param name="errorMessage_">Error Message to show [null = "Message default"]</param>
                                                /// <param name="suggestionMessage_">Suggestion to show [null = "Suggestion default"]</param>
                                                    this.ValidateInfo = function(input_, validationNameOrExpression_, errorMessage_, suggestionMessage_)
                                                    {
                                                        //Get current TextBox
                                                            var txtObj = $(input_);

                                                            var regularExpression = "";

                                                            var flagValid = false;

                                                            var validationInfo = { 'isValid': true, 'Field': '', 'Message': '' }

                                                        //Search for Validation to apply
                                                            switch (validationNameOrExpression_)
                                                            {
                                                                case "PasswordBase":
                                                                    regularExpression = /^[A-Za-z]\w{7,14}$/;
                                                                    flagValid = ((txtObj.val().match(regularExpression)) ? true : false);
                                                                    break;

                                                                case "Email":
                                                                    regularExpression = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                                                                    flagValid = ((txtObj.val().match(regularExpression)) ? true : false);
                                                                    break;

                                                                case "DecimalOrInteger":
                                                                    regularExpression = /^([0-9]*|[0-9]+\.[0-9]+)$/;
                                                                    flagValid = ((txtObj.val().match(regularExpression)) ? true : false);
                                                                    break;

                                                                case "Words":
                                                                    regularExpression = /^([a-zA-záéíóúÁÉÍÓÚ ]+)$/;
                                                                    flagValid = ((txtObj.val().match(regularExpression)) ? true : false);
                                                                    break;

                                                                case "Numbers":
                                                                    flagValid = (isNaN(txtObj.val()) ? false : true);
                                                                    break;

                                                                case "NotEmpty":

                                                                    flagValid = (txtObj.val().length == 0 ? false : true);

                                                                    break;

                                                                default:
                                                                    regularExpression = validationNameOrExpression_;
                                                                    break;
                                                            }

                                                            if (!flagValid) 
                                                            {
                                                                validationInfo.isValid = flagValid;
                                                                validationInfo.Field = txtObj.attr("ID");
                                                                validationInfo.Message = (errorMessage_ == null ? "The typed value for filed is not valid." : errorMessage_) + "<span style='color: rgba(11, 130, 21, 0.70);'>" + (suggestionMessage_ == null ? "Please verify value" : suggestionMessage_) + "</span>";
                                                            }
                                                            else
                                                            {
                                                                validationInfo = null;
                                                                validationInfo = true;
                                                            }

                                                            return validationInfo;
                                                    }

                                            //#endregion "-------------------------------------------- END MESSAGES -------------------------------------------

                                            //#region "-------------------------------------------- FORM BEHAVIORS --------------------------------------------

                                                /// <summary>
                                                /// Sets focus on a control specified
                                                /// <param name="parameters_">Json that contains the ID of control to set focus</param>
                                                /// </summary>
                                                    this.SetFocusOn = function(parameters_)
                                                    {
                                                        document.getElementById(parameters_.data.ControlID).focus();
                                                    }

                                                /// <summary>
                                                /// Refresh Curent View
                                                /// </summary>
                                                    this.RefreshView = function()
                                                    {
                                                        location.reload(true);
                                                    }

                                            //#endregion "---------------------------------------- END FORM BEHAVIORS ---------------------------------------
                            
                                            //#endregion "---------------------------------------------- HTML TABLES ---------------------------------------------

                                        }//End: JScripter Class 

                                } //End: ClientSideV2 NameSpace
                }
            
            }                
        //#endregion "----------------------- Class: RBS.JSs.ClientSideV2.JScripter ----------------------
