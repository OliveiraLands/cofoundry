angular.module('cms.setup').run(['$templateCache',function(t){t.put('/Admin/Modules/Setup/Js/Routes/SetupDetails.html','<cms-page-body ng-if="vm.isSetupComplete">    <cms-form-section cms-title="Setup complete">        <p>You\'re all set to go. Please follow the link below to login to Cofoundry.</p>        <div class="actions">            <a class="btn main-cta" href="{{::vm.urlLibrary.login()}}">Login</a>        </div>    </cms-form-section></cms-page-body><cms-form cms-name="mainForm"          cms-edit-mode="true"          ng-if="!vm.isSetupComplete"          ng-submit="vm.save()">    <cms-page-body cms-content-type="form-basic">        <cms-form-status></cms-form-status>        <cms-form-section cms-title="Site setup">            <p>Before we get started, we\'re gonna need some basic setup information from you. This will give your application a name and also create the default login account.</p>            <cms-form-field-text cms-title="Application Name"                                 cms-model="vm.command.applicationName"                                 maxlength="100"                                 required></cms-form-field-text>            <cms-form-field-text cms-title="First Name"                                 cms-model="vm.command.userFirstName"                                 maxlength="32"                                 required></cms-form-field-text>            <cms-form-field-text cms-title="Last Name"                                 cms-model="vm.command.userLastName"                                 maxlength="32"                                 required></cms-form-field-text>            <cms-form-field-email-address cms-title="Email"                                          cms-model="vm.command.userEmail"                                          maxlength="150"                                          required></cms-form-field-email-address>            <cms-form-field-password cms-title="Password"                                     cms-model="vm.command.userPassword"                                     minlength="8"                                     maxlength="300"                                     required></cms-form-field-password>            <cms-form-field-password cms-title="Confirm password"                                     cms-model="vm.command.confirmUserPassword"                                     cms-match="vm.command.userPassword"                                     cms-match-val-msg="Passwords must match"                                     maxlength="300"                                     required></cms-form-field-password>                        <div class="actions">                <cms-button-submit cms-text="Save"                       ng-disabled="vm.mainForm.$invalid || vm.saveLoadState.isLoading"                       cms-loading="vm.saveLoadState.isLoading"></cms-button-submit>            </div>        </cms-form-section>            </cms-page-body></cms-form>');}]);