 

namespace FSUIPC;

public class PMDG_777X_Offsets
{
	private static int ID;

	private string groupName;

	private object lockObject = new object();

	public Offset<byte>[] ICE_WindowHeatBackUp_Sw_OFF { get; private set; }

	public Offset<byte> ELEC_StandbyPowerSw { get; private set; }

	public Offset<byte>[] FCTL_WingHydValve_Sw_SHUT_OFF { get; private set; }

	public Offset<byte>[] FCTL_TailHydValve_Sw_SHUT_OFF { get; private set; }

	public Offset<byte>[] FCTL_annunTailHydVALVE_CLOSED { get; private set; }

	public Offset<byte>[] FCTL_annunWingHydVALVE_CLOSED { get; private set; }

	public Offset<byte> APU_Power_Sw_TEST { get; private set; }

	public Offset<byte>[] ENG_EECPower_Sw_TEST { get; private set; }

	public Offset<byte> ELEC_TowingPower_Sw_BATT { get; private set; }

	public Offset<byte> ELEC_annunTowingPowerON_BATT { get; private set; }

	public Offset<byte>[] AIR_CargoTemp_Selector { get; private set; }

	public Offset<byte> ADIRU_Sw_On { get; private set; }

	public Offset<byte> ADIRU_annunOFF { get; private set; }

	public Offset<byte> ADIRU_annunON_BAT { get; private set; }

	public Offset<byte> FCTL_ThrustAsymComp_Sw_AUTO { get; private set; }

	public Offset<byte> FCTL_annunThrustAsymCompOFF { get; private set; }

	public Offset<byte> ELEC_CabUtilSw { get; private set; }

	public Offset<byte> ELEC_annunCabUtilOFF { get; private set; }

	public Offset<byte> ELEC_IFEPassSeatsSw { get; private set; }

	public Offset<byte> ELEC_annunIFEPassSeatsOFF { get; private set; }

	public Offset<byte> ELEC_Battery_Sw_ON { get; private set; }

	public Offset<byte> ELEC_APUGen_Sw_ON { get; private set; }

	public Offset<byte> ELEC_APU_Selector { get; private set; }

	public Offset<byte> ELEC_annunAPU_FAULT { get; private set; }

	public Offset<byte>[] ELEC_BusTie_Sw_AUTO { get; private set; }

	public Offset<byte>[] ELEC_annunBusTieISLN { get; private set; }

	public Offset<byte>[] ELEC_ExtPwrSw { get; private set; }

	public Offset<byte>[] ELEC_annunExtPowr_ON { get; private set; }

	public Offset<byte>[] ELEC_annunExtPowr_AVAIL { get; private set; }

	public Offset<byte>[] ELEC_Gen_Sw_ON { get; private set; }

	public Offset<byte>[] ELEC_annunGenOFF { get; private set; }

	public Offset<byte>[] ELEC_BackupGen_Sw_ON { get; private set; }

	public Offset<byte>[] ELEC_annunBackupGenOFF { get; private set; }

	public Offset<byte>[] ELEC_IDGDiscSw { get; private set; }

	public Offset<byte>[] ELEC_annunIDGDiscDRIVE { get; private set; }

	public Offset<byte>[] WIPERS_Selector { get; private set; }

	public Offset<byte> LTS_EmerLightsSelector { get; private set; }

	public Offset<byte> COMM_ServiceInterphoneSw { get; private set; }

	public Offset<byte> OXY_PassOxygen_Sw_On { get; private set; }

	public Offset<byte> OXY_annunPassOxygenON { get; private set; }

	public Offset<byte>[] ICE_WindowHeat_Sw_ON { get; private set; }

	public Offset<byte>[] ICE_annunWindowHeatINOP { get; private set; }

	public Offset<byte> HYD_RamAirTurbineSw { get; private set; }

	public Offset<byte> HYD_annunRamAirTurbinePRESS { get; private set; }

	public Offset<byte> HYD_annunRamAirTurbineUNLKD { get; private set; }

	public Offset<byte>[] HYD_PrimaryEngPump_Sw_ON { get; private set; }

	public Offset<byte>[] HYD_PrimaryElecPump_Sw_ON { get; private set; }

	public Offset<byte>[] HYD_DemandElecPump_Selector { get; private set; }

	public Offset<byte>[] HYD_DemandAirPump_Selector { get; private set; }

	public Offset<byte>[] HYD_annunPrimaryEngPumpFAULT { get; private set; }

	public Offset<byte>[] HYD_annunPrimaryElecPumpFAULT { get; private set; }

	public Offset<byte>[] HYD_annunDemandElecPumpFAULT { get; private set; }

	public Offset<byte>[] HYD_annunDemandAirPumpFAULT { get; private set; }

	public Offset<byte> SIGNS_NoSmokingSelector { get; private set; }

	public Offset<byte> SIGNS_SeatBeltsSelector { get; private set; }

	public Offset<byte> LTS_DomeLightKnob { get; private set; }

	public Offset<byte> LTS_CircuitBreakerKnob { get; private set; }

	public Offset<byte> LTS_OvereadPanelKnob { get; private set; }

	public Offset<byte> LTS_GlareshieldPNLlKnob { get; private set; }

	public Offset<byte> LTS_GlareshieldFLOODKnob { get; private set; }

	public Offset<byte> LTS_Storm_Sw_ON { get; private set; }

	public Offset<byte> LTS_MasterBright_Sw_ON { get; private set; }

	public Offset<byte> LTS_MasterBrigntKnob { get; private set; }

	public Offset<byte> LTS_IndLightsTestSw { get; private set; }

	public Offset<byte>[] LTS_LandingLights_Sw_ON { get; private set; }

	public Offset<byte> LTS_Beacon_Sw_ON { get; private set; }

	public Offset<byte> LTS_NAV_Sw_ON { get; private set; }

	public Offset<byte> LTS_Logo_Sw_ON { get; private set; }

	public Offset<byte> LTS_Wing_Sw_ON { get; private set; }

	public Offset<byte>[] LTS_RunwayTurnoff_Sw_ON { get; private set; }

	public Offset<byte> LTS_Taxi_Sw_ON { get; private set; }

	public Offset<byte> LTS_Strobe_Sw_ON { get; private set; }

	public Offset<byte>[] FIRE_CargoFire_Sw_Arm { get; private set; }

	public Offset<byte>[] FIRE_annunCargoFire { get; private set; }

	public Offset<byte> FIRE_CargoFireDisch_Sw { get; private set; }

	public Offset<byte> FIRE_annunCargoDISCH { get; private set; }

	public Offset<byte> FIRE_FireOvhtTest_Sw { get; private set; }

	public Offset<byte> FIRE_APUHandle { get; private set; }

	public Offset<byte> FIRE_APUHandleUnlock_Sw { get; private set; }

	public Offset<byte> FIRE_annunAPU_BTL_DISCH { get; private set; }

	public Offset<byte>[] ENG_EECMode_Sw_NORM { get; private set; }

	public Offset<byte>[] ENG_Start_Selector { get; private set; }

	public Offset<byte> ENG_Autostart_Sw_ON { get; private set; }

	public Offset<byte>[] ENG_annunALTN { get; private set; }

	public Offset<byte> ENG_annunAutostartOFF { get; private set; }

	public Offset<byte> FUEL_CrossFeedFwd_Sw { get; private set; }

	public Offset<byte> FUEL_CrossFeedAft_Sw { get; private set; }

	public Offset<byte>[] FUEL_PumpFwd_Sw { get; private set; }

	public Offset<byte>[] FUEL_PumpAft_Sw { get; private set; }

	public Offset<byte>[] FUEL_PumpCtr_Sw { get; private set; }

	public Offset<byte>[] FUEL_JettisonNozle_Sw { get; private set; }

	public Offset<byte> FUEL_JettisonArm_Sw { get; private set; }

	public Offset<byte> FUEL_FuelToRemain_Sw_Pulled { get; private set; }

	public Offset<byte> FUEL_FuelToRemain_Selector { get; private set; }

	public Offset<byte> FUEL_annunFwdXFEED_VALVE { get; private set; }

	public Offset<byte> FUEL_annunAftXFEED_VALVE { get; private set; }

	public Offset<byte>[] FUEL_annunLOWPRESS_Fwd { get; private set; }

	public Offset<byte>[] FUEL_annunLOWPRESS_Aft { get; private set; }

	public Offset<byte>[] FUEL_annunLOWPRESS_Ctr { get; private set; }

	public Offset<byte>[] FUEL_annunJettisonNozleVALVE { get; private set; }

	public Offset<byte> FUEL_annunArmFAULT { get; private set; }

	public Offset<byte> ICE_WingAntiIceSw { get; private set; }

	public Offset<byte>[] ICE_EngAntiIceSw { get; private set; }

	public Offset<byte>[] AIR_Pack_Sw_AUTO { get; private set; }

	public Offset<byte>[] AIR_TrimAir_Sw_On { get; private set; }

	public Offset<byte>[] AIR_RecircFan_Sw_On { get; private set; }

	public Offset<byte>[] AIR_TempSelector { get; private set; }

	public Offset<byte> AIR_AirCondReset_Sw_Pushed { get; private set; }

	public Offset<byte> AIR_EquipCooling_Sw_AUTO { get; private set; }

	public Offset<byte> AIR_Gasper_Sw_On { get; private set; }

	public Offset<byte>[] AIR_annunPackOFF { get; private set; }

	public Offset<byte>[] AIR_annunTrimAirFAULT { get; private set; }

	public Offset<byte> AIR_annunEquipCoolingOVRD { get; private set; }

	public Offset<byte>[] AIR_EngBleedAir_Sw_AUTO { get; private set; }

	public Offset<byte> AIR_APUBleedAir_Sw_AUTO { get; private set; }

	public Offset<byte>[] AIR_IsolationValve_Sw { get; private set; }

	public Offset<byte> AIR_CtrIsolationValve_Sw { get; private set; }

	public Offset<byte>[] AIR_annunEngBleedAirOFF { get; private set; }

	public Offset<byte> AIR_annunAPUBleedAirOFF { get; private set; }

	public Offset<byte>[] AIR_annunIsolationValveCLOSED { get; private set; }

	public Offset<byte> AIR_annunCtrIsolationValveCLOSED { get; private set; }

	public Offset<byte>[] AIR_OutflowValve_Sw_AUTO { get; private set; }

	public Offset<byte>[] AIR_OutflowValveManual_Selector { get; private set; }

	public Offset<byte> AIR_LdgAlt_Sw_Pulled { get; private set; }

	public Offset<byte> AIR_LdgAlt_Selector { get; private set; }

	public Offset<byte>[] AIR_annunOutflowValve_MAN { get; private set; }

	public Offset<byte> GEAR_Lever { get; private set; }

	public Offset<byte> GEAR_LockOvrd_Sw { get; private set; }

	public Offset<byte> GEAR_AltnGear_Sw_DOWN { get; private set; }

	public Offset<byte> GPWS_FlapInhibitSw_OVRD { get; private set; }

	public Offset<byte> GPWS_GearInhibitSw_OVRD { get; private set; }

	public Offset<byte> GPWS_TerrInhibitSw_OVRD { get; private set; }

	public Offset<byte> GPWS_GSInhibit_Sw { get; private set; }

	public Offset<byte> GPWS_annunGND_PROX_top { get; private set; }

	public Offset<byte> GPWS_annunGND_PROX_bottom { get; private set; }

	public Offset<byte> BRAKES_AutobrakeSelector { get; private set; }

	public Offset<byte> ISFD_Baro_Sw_Pushed { get; private set; }

	public Offset<byte> ISFD_RST_Sw_Pushed { get; private set; }

	public Offset<byte> ISFD_Minus_Sw_Pushed { get; private set; }

	public Offset<byte> ISFD_Plus_Sw_Pushed { get; private set; }

	public Offset<byte> ISFD_APP_Sw_Pushed { get; private set; }

	public Offset<byte> ISFD_HP_IN_Sw_Pushed { get; private set; }

	public Offset<byte> ISP_Nav_L_Sw_CDU { get; private set; }

	public Offset<byte> ISP_DsplCtrl_L_Sw_Altn { get; private set; }

	public Offset<byte> ISP_AirDataAtt_L_Sw_Altn { get; private set; }

	public Offset<byte> DSP_InbdDspl_L_Selector { get; private set; }

	public Offset<byte> EFIS_HdgRef_Sw_Norm { get; private set; }

	public Offset<byte> EFIS_annunHdgRefTRUE { get; private set; }

	public Offset<uint> BRAKES_BrakePressNeedle { get; private set; }

	public Offset<byte> BRAKES_annunBRAKE_SOURCE { get; private set; }

	public Offset<byte> ISP_Nav_R_Sw_CDU { get; private set; }

	public Offset<byte> ISP_DsplCtrl_R_Sw_Altn { get; private set; }

	public Offset<byte> ISP_AirDataAtt_R_Sw_Altn { get; private set; }

	public Offset<byte> ISP_FMC_Selector { get; private set; }

	public Offset<byte> DSP_InbdDspl_R_Selector { get; private set; }

	public Offset<byte>[] AIR_ShoulderHeaterKnob { get; private set; }

	public Offset<byte>[] AIR_FootHeaterSelector { get; private set; }

	public Offset<byte> LTS_LeftFwdPanelPNLKnob { get; private set; }

	public Offset<byte> LTS_LeftFwdPanelFLOODKnob { get; private set; }

	public Offset<byte> LTS_LeftOutbdDsplBRIGHTNESSKnob { get; private set; }

	public Offset<byte> LTS_LeftInbdDsplBRIGHTNESSKnob { get; private set; }

	public Offset<byte> LTS_RightFwdPanelPNLKnob { get; private set; }

	public Offset<byte> LTS_RightFwdPanelFLOODKnob { get; private set; }

	public Offset<byte> LTS_RightInbdDsplBRIGHTNESSKnob { get; private set; }

	public Offset<byte> LTS_RightOutbdDsplBRIGHTNESSKnob { get; private set; }

	public Offset<byte>[] CHR_Chr_Sw_Pushed { get; private set; }

	public Offset<byte>[] CHR_TimeDate_Sw_Pushed { get; private set; }

	public Offset<byte>[] CHR_TimeDate_Selector { get; private set; }

	public Offset<byte>[] CHR_Set_Selector { get; private set; }

	public Offset<byte>[] CHR_ET_Selector { get; private set; }

	public Offset<byte>[] EFIS_MinsSelBARO { get; private set; }

	public Offset<byte>[] EFIS_BaroSelHPA { get; private set; }

	public Offset<byte>[] EFIS_VORADFSel1 { get; private set; }

	public Offset<byte>[] EFIS_VORADFSel2 { get; private set; }

	public Offset<byte>[] EFIS_ModeSel { get; private set; }

	public Offset<byte>[] EFIS_RangeSel { get; private set; }

	public Offset<byte>[] EFIS_MinsKnob { get; private set; }

	public Offset<byte>[] EFIS_BaroKnob { get; private set; }

	public Offset<byte>[] EFIS_MinsRST_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_BaroSTD_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_ModeCTR_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_RangeTFC_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_WXR_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_STA_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_WPT_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_ARPT_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_DATA_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_POS_Sw_Pushed { get; private set; }

	public Offset<byte>[] EFIS_TERR_Sw_Pushed { get; private set; }

	public Offset<float> MCP_IASMach { get; private set; }

	public Offset<byte> MCP_IASBlan { get; private set; }

	public Offset<ushort> MCP_Heading { get; private set; }

	public Offset<ushort> MCP_Altitude { get; private set; }

	public Offset<short> MCP_VertSpeed { get; private set; }

	public Offset<float> MCP_FPA { get; private set; }

	public Offset<byte> MCP_VertSpeedBlank { get; private set; }

	public Offset<byte>[] MCP_FD_Sw_On { get; private set; }

	public Offset<byte>[] MCP_ATArm_Sw_On { get; private set; }

	public Offset<byte> MCP_BankLimitSel { get; private set; }

	public Offset<byte> MCP_AltIncrSel { get; private set; }

	public Offset<byte> MCP_DisengageBar { get; private set; }

	public Offset<byte> MCP_Speed_Dial { get; private set; }

	public Offset<byte> MCP_Heading_Dial { get; private set; }

	public Offset<byte> MCP_Altitude_Dial { get; private set; }

	public Offset<byte> MCP_VS_Wheel { get; private set; }

	public Offset<byte> MCP_HDGDial_Mode { get; private set; }

	public Offset<byte> MCP_VSDial_Mode { get; private set; }

	public Offset<byte>[] MCP_AP_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_CLB_CON_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_AT_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_LNAV_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_VNAV_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_FLCH_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_HDG_HOLD_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_VS_FPA_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_ALT_HOLD_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_LOC_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_APP_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_Speeed_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_Heading_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_Altitude_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_IAS_MACH_Toggle_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_HDG_TRK_Toggle_Sw_Pushed { get; private set; }

	public Offset<byte> MCP_VS_FPA_Toggle_Sw_Pushed { get; private set; }

	public Offset<byte>[] MCP_annunAP { get; private set; }

	public Offset<byte> MCP_annunAT { get; private set; }

	public Offset<byte> MCP_annunLNAV { get; private set; }

	public Offset<byte> MCP_annunVNAV { get; private set; }

	public Offset<byte> MCP_annunFLCH { get; private set; }

	public Offset<byte> MCP_annunHDG_HOLD { get; private set; }

	public Offset<byte> MCP_annunVS_FPA { get; private set; }

	public Offset<byte> MCP_annunALT_HOLD { get; private set; }

	public Offset<byte> MCP_annunLOC { get; private set; }

	public Offset<byte> MCP_annunAPP { get; private set; }

	public Offset<byte> DSP_L_INBD_Sw { get; private set; }

	public Offset<byte> DSP_R_INBD_Sw { get; private set; }

	public Offset<byte> DSP_LWR_CTR_Sw { get; private set; }

	public Offset<byte> DSP_ENG_Sw { get; private set; }

	public Offset<byte> DSP_STAT_Sw { get; private set; }

	public Offset<byte> DSP_ELEC_Sw { get; private set; }

	public Offset<byte> DSP_HYD_Sw { get; private set; }

	public Offset<byte> DSP_FUEL_Sw { get; private set; }

	public Offset<byte> DSP_AIR_Sw { get; private set; }

	public Offset<byte> DSP_DOOR_Sw { get; private set; }

	public Offset<byte> DSP_GEAR_Sw { get; private set; }

	public Offset<byte> DSP_FCTL_Sw { get; private set; }

	public Offset<byte> DSP_CAM_Sw { get; private set; }

	public Offset<byte> DSP_CHKL_Sw { get; private set; }

	public Offset<byte> DSP_COMM_Sw { get; private set; }

	public Offset<byte> DSP_NAV_Sw { get; private set; }

	public Offset<byte> DSP_CANC_RCL_Sw { get; private set; }

	public Offset<byte> DSP_annunL_INBD { get; private set; }

	public Offset<byte> DSP_annunR_INBD { get; private set; }

	public Offset<byte> DSP_annunLWR_CTR { get; private set; }

	public Offset<byte>[] WARN_Reset_Sw_Pushed { get; private set; }

	public Offset<byte>[] WARN_annunMASTER_WARNING { get; private set; }

	public Offset<byte>[] WARN_annunMASTER_CAUTION { get; private set; }

	public Offset<byte> ISP_DsplCtrl_C_Sw_Altn { get; private set; }

	public Offset<byte> LTS_UpperDsplBRIGHTNESSKnob { get; private set; }

	public Offset<byte> LTS_LowerDsplBRIGHTNESSKnob { get; private set; }

	public Offset<byte> EICAS_EventRcd_Sw_Pushed { get; private set; }

	public Offset<byte>[] CDU_annunEXEC { get; private set; }

	public Offset<byte>[] CDU_annunDSPY { get; private set; }

	public Offset<byte>[] CDU_annunFAIL { get; private set; }

	public Offset<byte>[] CDU_annunMSG { get; private set; }

	public Offset<byte>[] CDU_annunOFST { get; private set; }

	public Offset<byte>[] CDU_BrtKnob { get; private set; }

	public Offset<byte> FCTL_AltnFlaps_Sw_ARM { get; private set; }

	public Offset<byte> FCTL_AltnFlaps_Control_Sw { get; private set; }

	public Offset<byte> FCTL_StabCutOutSw_C_NORMAL { get; private set; }

	public Offset<byte> FCTL_StabCutOutSw_R_NORMAL { get; private set; }

	public Offset<byte> FCTL_AltnPitch_Lever { get; private set; }

	public Offset<byte> FCTL_Speedbrake_Lever { get; private set; }

	public Offset<byte> FCTL_Flaps_Lever { get; private set; }

	public Offset<byte>[] ENG_FuelControl_Sw_RUN { get; private set; }

	public Offset<byte> BRAKES_ParkingBrakeLeverOn { get; private set; }

	public Offset<byte>[] COMM_SelectedMic { get; private set; }

	public Offset<byte>[] COMM_ReceiverSwitches { get; private set; }

	public Offset<byte> COMM_OBSAudio_Selector { get; private set; }

	public Offset<byte>[] COMM_SelectedRadio { get; private set; }

	public Offset<byte>[] COMM_RadioTransfer_Sw_Pushed { get; private set; }

	public Offset<byte>[] COMM_RadioPanelOff { get; private set; }

	public Offset<byte>[] COMM_annunAM { get; private set; }

	public Offset<byte> XPDR_XpndrSelector_R { get; private set; }

	public Offset<byte> XPDR_AltSourceSel_ALTN { get; private set; }

	public Offset<byte> XPDR_ModeSel { get; private set; }

	public Offset<byte> XPDR_Ident_Sw_Pushed { get; private set; }

	public Offset<byte>[] FIRE_EngineHandle { get; private set; }

	public Offset<byte>[] FIRE_EngineHandleUnlock_Sw { get; private set; }

	public Offset<byte>[] FIRE_annunENG_BTL_DISCH { get; private set; }

	public Offset<byte> FCTL_AileronTrim_Switches { get; private set; }

	public Offset<byte> FCTL_RudderTrim_Knob { get; private set; }

	public Offset<byte> FCTL_RudderTrimCancel_Sw_Pushed { get; private set; }

	public Offset<byte> EVAC_Command_Sw_ON { get; private set; }

	public Offset<byte> EVAC_PressToTest_Sw_Pressed { get; private set; }

	public Offset<byte> EVAC_HornSutOff_Sw_Pulled { get; private set; }

	public Offset<byte> EVAC_LightIlluminated { get; private set; }

	public Offset<byte> LTS_AisleStandPNLKnob { get; private set; }

	public Offset<byte> LTS_AisleStandFLOODKnob { get; private set; }

	public Offset<byte> LTS_FloorLightsSw { get; private set; }

	public Offset<byte>[] DOOR_state { get; private set; }

	public Offset<byte>[] ENG_StartValve { get; private set; }

	public Offset<float>[] AIR_DuctPress { get; private set; }

	public Offset<float> FUEL_QtyCenter { get; private set; }

	public Offset<float> FUEL_QtyLeft { get; private set; }

	public Offset<float> FUEL_QtyRight { get; private set; }

	public Offset<float> FUEL_QtyAux { get; private set; }

	public Offset<byte> IRS_aligned { get; private set; }

	public Offset<byte> AircraftModel { get; private set; }

	public Offset<byte> WeightInKg { get; private set; }

	public Offset<byte> GPWS_V1CallEnabled { get; private set; }

	public Offset<byte> GroundConnAvailable { get; private set; }

	public Offset<byte> FMC_TakeoffFlaps { get; private set; }

	public Offset<byte> FMC_V1 { get; private set; }

	public Offset<byte> FMC_VR { get; private set; }

	public Offset<byte> FMC_V2 { get; private set; }

	public Offset<byte> FMC_LandingFlaps { get; private set; }

	public Offset<byte> FMC_LandingVREF { get; private set; }

	public Offset<ushort> FMC_CruiseAlt { get; private set; }

	public Offset<short> FMC_LandingAltitude { get; private set; }

	public Offset<ushort> FMC_TransitionAlt { get; private set; }

	public Offset<ushort> FMC_TransitionLevel { get; private set; }

	public Offset<byte> FMC_PerfInputComplete { get; private set; }

	public Offset<float> FMC_DistanceToTOD { get; private set; }

	public Offset<float> FMC_DistanceToDest { get; private set; }

	public Offset<string> FMC_flightNumber { get; private set; }

	public Offset<byte> AIR_AltnVentSw_ON { get; private set; }

	public Offset<byte> AIR_annunAltnVentFAULT { get; private set; }

	public Offset<byte> AIR_CargoTemp_MainDeckFwd_Sel { get; private set; }

	public Offset<byte> AIR_CargoTemp_MainDeckAft_Sel { get; private set; }

	public Offset<byte> AIR_CargoTemp_LowerFwd_Sel { get; private set; }

	public Offset<byte> AIR_CargoTemp_LowerAft_Sel { get; private set; }

	public Offset<byte> ELEC_annunAPU_GEN_OFF { get; private set; }

	public Offset<byte> AIR_MainDeckFlowSw_NORM { get; private set; }

	public Offset<byte> ELEC_annunBattery_OFF { get; private set; }

	public Offset<byte> FCTL_PrimFltComputersSw_AUTO { get; private set; }

	public Offset<byte> FCTL_annunPrimFltComputersDISC { get; private set; }

	public Offset<byte>[] FIRE_EngineHandleIlluminated { get; private set; }

	public Offset<byte> FIRE_APUHandleIlluminated { get; private set; }

	public Offset<byte>[] FIRE_EngineHandleIsUnlocked { get; private set; }

	public Offset<byte> FIRE_APUHandleIsUnlocked { get; private set; }

	public Offset<byte> FIRE_annunMainDeckCargoFire { get; private set; }

	public Offset<byte> FIRE_annunCargoDEPR { get; private set; }

	public Offset<byte> GPWS_RunwayOvrdSw_OVRD { get; private set; }

	public Offset<ushort>[] COMM_ReceiverSwitches_NEW { get; private set; }

	public Offset<byte> WheelChocksSet { get; private set; }

	public Offset<byte> APURunning { get; private set; }

	public PMDG_777X_Offsets()
	{
		ID++;
		groupName = "~~~ PMDG 777 OFFSETS " + ID + " ~~~";
		ICE_WindowHeatBackUp_Sw_OFF = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25632),
			new Offset<byte>(groupName, 25633)
		};
		ELEC_StandbyPowerSw = new Offset<byte>(groupName, 25634);
		FCTL_WingHydValve_Sw_SHUT_OFF = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 25635),
			new Offset<byte>(groupName, 25636),
			new Offset<byte>(groupName, 25637)
		};
		FCTL_TailHydValve_Sw_SHUT_OFF = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 25638),
			new Offset<byte>(groupName, 25639),
			new Offset<byte>(groupName, 25640)
		};
		FCTL_annunTailHydVALVE_CLOSED = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 25641),
			new Offset<byte>(groupName, 25642),
			new Offset<byte>(groupName, 25643)
		};
		FCTL_annunWingHydVALVE_CLOSED = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 25644),
			new Offset<byte>(groupName, 25645),
			new Offset<byte>(groupName, 25646)
		};
		APU_Power_Sw_TEST = new Offset<byte>(groupName, 25647);
		ENG_EECPower_Sw_TEST = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25648),
			new Offset<byte>(groupName, 25649)
		};
		ELEC_TowingPower_Sw_BATT = new Offset<byte>(groupName, 25650);
		ELEC_annunTowingPowerON_BATT = new Offset<byte>(groupName, 25651);
		AIR_CargoTemp_Selector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25652),
			new Offset<byte>(groupName, 25653)
		};
		ADIRU_Sw_On = new Offset<byte>(groupName, 25654);
		ADIRU_annunOFF = new Offset<byte>(groupName, 25655);
		ADIRU_annunON_BAT = new Offset<byte>(groupName, 25656);
		FCTL_ThrustAsymComp_Sw_AUTO = new Offset<byte>(groupName, 25657);
		FCTL_annunThrustAsymCompOFF = new Offset<byte>(groupName, 25658);
		ELEC_CabUtilSw = new Offset<byte>(groupName, 25659);
		ELEC_annunCabUtilOFF = new Offset<byte>(groupName, 25660);
		ELEC_IFEPassSeatsSw = new Offset<byte>(groupName, 25661);
		ELEC_annunIFEPassSeatsOFF = new Offset<byte>(groupName, 25662);
		ELEC_Battery_Sw_ON = new Offset<byte>(groupName, 25663);
		ELEC_APUGen_Sw_ON = new Offset<byte>(groupName, 25664);
		ELEC_APU_Selector = new Offset<byte>(groupName, 25665);
		ELEC_annunAPU_FAULT = new Offset<byte>(groupName, 25666);
		ELEC_BusTie_Sw_AUTO = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25667),
			new Offset<byte>(groupName, 25668)
		};
		ELEC_annunBusTieISLN = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25669),
			new Offset<byte>(groupName, 25670)
		};
		ELEC_ExtPwrSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25671),
			new Offset<byte>(groupName, 25672)
		};
		ELEC_annunExtPowr_ON = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25673),
			new Offset<byte>(groupName, 25674)
		};
		ELEC_annunExtPowr_AVAIL = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25675),
			new Offset<byte>(groupName, 25676)
		};
		ELEC_Gen_Sw_ON = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25677),
			new Offset<byte>(groupName, 25678)
		};
		ELEC_annunGenOFF = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25679),
			new Offset<byte>(groupName, 25680)
		};
		ELEC_BackupGen_Sw_ON = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25681),
			new Offset<byte>(groupName, 25682)
		};
		ELEC_annunBackupGenOFF = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25683),
			new Offset<byte>(groupName, 25684)
		};
		ELEC_IDGDiscSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25685),
			new Offset<byte>(groupName, 25686)
		};
		ELEC_annunIDGDiscDRIVE = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25687),
			new Offset<byte>(groupName, 25688)
		};
		WIPERS_Selector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25689),
			new Offset<byte>(groupName, 25690)
		};
		LTS_EmerLightsSelector = new Offset<byte>(groupName, 25691);
		COMM_ServiceInterphoneSw = new Offset<byte>(groupName, 25692);
		OXY_PassOxygen_Sw_On = new Offset<byte>(groupName, 25693);
		OXY_annunPassOxygenON = new Offset<byte>(groupName, 25694);
		ICE_WindowHeat_Sw_ON = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25695),
			new Offset<byte>(groupName, 25696),
			new Offset<byte>(groupName, 25697),
			new Offset<byte>(groupName, 25698)
		};
		ICE_annunWindowHeatINOP = new Offset<byte>[4]
		{
			new Offset<byte>(groupName, 25699),
			new Offset<byte>(groupName, 25700),
			new Offset<byte>(groupName, 25701),
			new Offset<byte>(groupName, 25702)
		};
		HYD_RamAirTurbineSw = new Offset<byte>(groupName, 25703);
		HYD_annunRamAirTurbinePRESS = new Offset<byte>(groupName, 25704);
		HYD_annunRamAirTurbineUNLKD = new Offset<byte>(groupName, 25705);
		HYD_PrimaryEngPump_Sw_ON = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25706),
			new Offset<byte>(groupName, 25707)
		};
		HYD_PrimaryElecPump_Sw_ON = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25708),
			new Offset<byte>(groupName, 25709)
		};
		HYD_DemandElecPump_Selector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25710),
			new Offset<byte>(groupName, 25711)
		};
		HYD_DemandAirPump_Selector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25712),
			new Offset<byte>(groupName, 25713)
		};
		HYD_annunPrimaryEngPumpFAULT = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25714),
			new Offset<byte>(groupName, 25715)
		};
		HYD_annunPrimaryElecPumpFAULT = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25716),
			new Offset<byte>(groupName, 25717)
		};
		HYD_annunDemandElecPumpFAULT = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25718),
			new Offset<byte>(groupName, 25719)
		};
		HYD_annunDemandAirPumpFAULT = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25720),
			new Offset<byte>(groupName, 25721)
		};
		SIGNS_NoSmokingSelector = new Offset<byte>(groupName, 25722);
		SIGNS_SeatBeltsSelector = new Offset<byte>(groupName, 25723);
		LTS_DomeLightKnob = new Offset<byte>(groupName, 25724);
		LTS_CircuitBreakerKnob = new Offset<byte>(groupName, 25725);
		LTS_OvereadPanelKnob = new Offset<byte>(groupName, 25726);
		LTS_GlareshieldPNLlKnob = new Offset<byte>(groupName, 25727);
		LTS_GlareshieldFLOODKnob = new Offset<byte>(groupName, 25728);
		LTS_Storm_Sw_ON = new Offset<byte>(groupName, 25729);
		LTS_MasterBright_Sw_ON = new Offset<byte>(groupName, 25730);
		LTS_MasterBrigntKnob = new Offset<byte>(groupName, 25731);
		LTS_IndLightsTestSw = new Offset<byte>(groupName, 25732);
		LTS_LandingLights_Sw_ON = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 25733),
			new Offset<byte>(groupName, 25734),
			new Offset<byte>(groupName, 25735)
		};
		LTS_Beacon_Sw_ON = new Offset<byte>(groupName, 25736);
		LTS_NAV_Sw_ON = new Offset<byte>(groupName, 25737);
		LTS_Logo_Sw_ON = new Offset<byte>(groupName, 25738);
		LTS_Wing_Sw_ON = new Offset<byte>(groupName, 25739);
		LTS_RunwayTurnoff_Sw_ON = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25740),
			new Offset<byte>(groupName, 25741)
		};
		LTS_Taxi_Sw_ON = new Offset<byte>(groupName, 25742);
		LTS_Strobe_Sw_ON = new Offset<byte>(groupName, 25743);
		FIRE_CargoFire_Sw_Arm = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25744),
			new Offset<byte>(groupName, 25745)
		};
		FIRE_annunCargoFire = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25746),
			new Offset<byte>(groupName, 25747)
		};
		FIRE_CargoFireDisch_Sw = new Offset<byte>(groupName, 25748);
		FIRE_annunCargoDISCH = new Offset<byte>(groupName, 25749);
		FIRE_FireOvhtTest_Sw = new Offset<byte>(groupName, 25750);
		FIRE_APUHandle = new Offset<byte>(groupName, 25751);
		FIRE_APUHandleUnlock_Sw = new Offset<byte>(groupName, 25752);
		FIRE_annunAPU_BTL_DISCH = new Offset<byte>(groupName, 25753);
		ENG_EECMode_Sw_NORM = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25754),
			new Offset<byte>(groupName, 25755)
		};
		ENG_Start_Selector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25756),
			new Offset<byte>(groupName, 25757)
		};
		ENG_Autostart_Sw_ON = new Offset<byte>(groupName, 25758);
		ENG_annunALTN = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25759),
			new Offset<byte>(groupName, 25760)
		};
		ENG_annunAutostartOFF = new Offset<byte>(groupName, 25761);
		FUEL_CrossFeedFwd_Sw = new Offset<byte>(groupName, 25762);
		FUEL_CrossFeedAft_Sw = new Offset<byte>(groupName, 25763);
		FUEL_PumpFwd_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25764),
			new Offset<byte>(groupName, 25765)
		};
		FUEL_PumpAft_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25766),
			new Offset<byte>(groupName, 25767)
		};
		FUEL_PumpCtr_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25768),
			new Offset<byte>(groupName, 25769)
		};
		FUEL_JettisonNozle_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25770),
			new Offset<byte>(groupName, 25771)
		};
		FUEL_JettisonArm_Sw = new Offset<byte>(groupName, 25772);
		FUEL_FuelToRemain_Sw_Pulled = new Offset<byte>(groupName, 25773);
		FUEL_FuelToRemain_Selector = new Offset<byte>(groupName, 25774);
		FUEL_annunFwdXFEED_VALVE = new Offset<byte>(groupName, 25775);
		FUEL_annunAftXFEED_VALVE = new Offset<byte>(groupName, 25776);
		FUEL_annunLOWPRESS_Fwd = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25777),
			new Offset<byte>(groupName, 25778)
		};
		FUEL_annunLOWPRESS_Aft = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25779),
			new Offset<byte>(groupName, 25780)
		};
		FUEL_annunLOWPRESS_Ctr = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25781),
			new Offset<byte>(groupName, 25782)
		};
		FUEL_annunJettisonNozleVALVE = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25783),
			new Offset<byte>(groupName, 25784)
		};
		FUEL_annunArmFAULT = new Offset<byte>(groupName, 25785);
		ICE_WingAntiIceSw = new Offset<byte>(groupName, 25786);
		ICE_EngAntiIceSw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25787),
			new Offset<byte>(groupName, 25788)
		};
		AIR_Pack_Sw_AUTO = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25789),
			new Offset<byte>(groupName, 25790)
		};
		AIR_TrimAir_Sw_On = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25791),
			new Offset<byte>(groupName, 25792)
		};
		AIR_RecircFan_Sw_On = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25793),
			new Offset<byte>(groupName, 25794)
		};
		AIR_TempSelector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25795),
			new Offset<byte>(groupName, 25796)
		};
		AIR_AirCondReset_Sw_Pushed = new Offset<byte>(groupName, 25797);
		AIR_EquipCooling_Sw_AUTO = new Offset<byte>(groupName, 25798);
		AIR_Gasper_Sw_On = new Offset<byte>(groupName, 25799);
		AIR_annunPackOFF = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25800),
			new Offset<byte>(groupName, 25801)
		};
		AIR_annunTrimAirFAULT = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25802),
			new Offset<byte>(groupName, 25803)
		};
		AIR_annunEquipCoolingOVRD = new Offset<byte>(groupName, 25804);
		AIR_EngBleedAir_Sw_AUTO = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25805),
			new Offset<byte>(groupName, 25806)
		};
		AIR_APUBleedAir_Sw_AUTO = new Offset<byte>(groupName, 25807);
		AIR_IsolationValve_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25808),
			new Offset<byte>(groupName, 25809)
		};
		AIR_CtrIsolationValve_Sw = new Offset<byte>(groupName, 25810);
		AIR_annunEngBleedAirOFF = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25811),
			new Offset<byte>(groupName, 25812)
		};
		AIR_annunAPUBleedAirOFF = new Offset<byte>(groupName, 25813);
		AIR_annunIsolationValveCLOSED = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25814),
			new Offset<byte>(groupName, 25815)
		};
		AIR_annunCtrIsolationValveCLOSED = new Offset<byte>(groupName, 25816);
		AIR_OutflowValve_Sw_AUTO = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25817),
			new Offset<byte>(groupName, 25818)
		};
		AIR_OutflowValveManual_Selector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25819),
			new Offset<byte>(groupName, 25820)
		};
		AIR_LdgAlt_Sw_Pulled = new Offset<byte>(groupName, 25821);
		AIR_LdgAlt_Selector = new Offset<byte>(groupName, 25822);
		AIR_annunOutflowValve_MAN = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25823),
			new Offset<byte>(groupName, 25824)
		};
		GEAR_Lever = new Offset<byte>(groupName, 25825);
		GEAR_LockOvrd_Sw = new Offset<byte>(groupName, 25826);
		GEAR_AltnGear_Sw_DOWN = new Offset<byte>(groupName, 25827);
		GPWS_FlapInhibitSw_OVRD = new Offset<byte>(groupName, 25828);
		GPWS_GearInhibitSw_OVRD = new Offset<byte>(groupName, 25829);
		GPWS_TerrInhibitSw_OVRD = new Offset<byte>(groupName, 25830);
		GPWS_GSInhibit_Sw = new Offset<byte>(groupName, 25831);
		GPWS_annunGND_PROX_top = new Offset<byte>(groupName, 25832);
		GPWS_annunGND_PROX_bottom = new Offset<byte>(groupName, 25833);
		BRAKES_AutobrakeSelector = new Offset<byte>(groupName, 25834);
		ISFD_Baro_Sw_Pushed = new Offset<byte>(groupName, 25835);
		ISFD_RST_Sw_Pushed = new Offset<byte>(groupName, 25836);
		ISFD_Minus_Sw_Pushed = new Offset<byte>(groupName, 25837);
		ISFD_Plus_Sw_Pushed = new Offset<byte>(groupName, 25838);
		ISFD_APP_Sw_Pushed = new Offset<byte>(groupName, 25839);
		ISFD_HP_IN_Sw_Pushed = new Offset<byte>(groupName, 25840);
		ISP_Nav_L_Sw_CDU = new Offset<byte>(groupName, 25841);
		ISP_DsplCtrl_L_Sw_Altn = new Offset<byte>(groupName, 25842);
		ISP_AirDataAtt_L_Sw_Altn = new Offset<byte>(groupName, 25843);
		DSP_InbdDspl_L_Selector = new Offset<byte>(groupName, 25844);
		EFIS_HdgRef_Sw_Norm = new Offset<byte>(groupName, 25845);
		EFIS_annunHdgRefTRUE = new Offset<byte>(groupName, 25846);
		BRAKES_BrakePressNeedle = new Offset<uint>(groupName, 25848);
		BRAKES_annunBRAKE_SOURCE = new Offset<byte>(groupName, 25852);
		ISP_Nav_R_Sw_CDU = new Offset<byte>(groupName, 25853);
		ISP_DsplCtrl_R_Sw_Altn = new Offset<byte>(groupName, 25854);
		ISP_AirDataAtt_R_Sw_Altn = new Offset<byte>(groupName, 25855);
		ISP_FMC_Selector = new Offset<byte>(groupName, 25856);
		DSP_InbdDspl_R_Selector = new Offset<byte>(groupName, 25857);
		AIR_ShoulderHeaterKnob = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25858),
			new Offset<byte>(groupName, 25859)
		};
		AIR_FootHeaterSelector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25860),
			new Offset<byte>(groupName, 25861)
		};
		LTS_LeftFwdPanelPNLKnob = new Offset<byte>(groupName, 25862);
		LTS_LeftFwdPanelFLOODKnob = new Offset<byte>(groupName, 25863);
		LTS_LeftOutbdDsplBRIGHTNESSKnob = new Offset<byte>(groupName, 25864);
		LTS_LeftInbdDsplBRIGHTNESSKnob = new Offset<byte>(groupName, 25865);
		LTS_RightFwdPanelPNLKnob = new Offset<byte>(groupName, 25866);
		LTS_RightFwdPanelFLOODKnob = new Offset<byte>(groupName, 25867);
		LTS_RightInbdDsplBRIGHTNESSKnob = new Offset<byte>(groupName, 25868);
		LTS_RightOutbdDsplBRIGHTNESSKnob = new Offset<byte>(groupName, 25869);
		CHR_Chr_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25870),
			new Offset<byte>(groupName, 25871)
		};
		CHR_TimeDate_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25872),
			new Offset<byte>(groupName, 25873)
		};
		CHR_TimeDate_Selector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25874),
			new Offset<byte>(groupName, 25875)
		};
		CHR_Set_Selector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25876),
			new Offset<byte>(groupName, 25877)
		};
		CHR_ET_Selector = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25878),
			new Offset<byte>(groupName, 25879)
		};
		EFIS_MinsSelBARO = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25880),
			new Offset<byte>(groupName, 25881)
		};
		EFIS_BaroSelHPA = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25882),
			new Offset<byte>(groupName, 25883)
		};
		EFIS_VORADFSel1 = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25884),
			new Offset<byte>(groupName, 25885)
		};
		EFIS_VORADFSel2 = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25886),
			new Offset<byte>(groupName, 25887)
		};
		EFIS_ModeSel = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25888),
			new Offset<byte>(groupName, 25889)
		};
		EFIS_RangeSel = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25890),
			new Offset<byte>(groupName, 25891)
		};
		EFIS_MinsKnob = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25892),
			new Offset<byte>(groupName, 25893)
		};
		EFIS_BaroKnob = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25894),
			new Offset<byte>(groupName, 25895)
		};
		EFIS_MinsRST_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25896),
			new Offset<byte>(groupName, 25897)
		};
		EFIS_BaroSTD_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25898),
			new Offset<byte>(groupName, 25899)
		};
		EFIS_ModeCTR_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25900),
			new Offset<byte>(groupName, 25901)
		};
		EFIS_RangeTFC_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25902),
			new Offset<byte>(groupName, 25903)
		};
		EFIS_WXR_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25904),
			new Offset<byte>(groupName, 25905)
		};
		EFIS_STA_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25906),
			new Offset<byte>(groupName, 25907)
		};
		EFIS_WPT_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25908),
			new Offset<byte>(groupName, 25909)
		};
		EFIS_ARPT_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25910),
			new Offset<byte>(groupName, 25911)
		};
		EFIS_DATA_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25912),
			new Offset<byte>(groupName, 25913)
		};
		EFIS_POS_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25914),
			new Offset<byte>(groupName, 25915)
		};
		EFIS_TERR_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25916),
			new Offset<byte>(groupName, 25917)
		};
		MCP_IASMach = new Offset<float>(groupName, 25920);
		MCP_IASBlan = new Offset<byte>(groupName, 25924);
		MCP_Heading = new Offset<ushort>(groupName, 25926);
		MCP_Altitude = new Offset<ushort>(groupName, 25928);
		MCP_VertSpeed = new Offset<short>(groupName, 25930);
		MCP_FPA = new Offset<float>(groupName, 25932);
		MCP_VertSpeedBlank = new Offset<byte>(groupName, 25936);
		MCP_FD_Sw_On = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25937),
			new Offset<byte>(groupName, 25938)
		};
		MCP_ATArm_Sw_On = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25939),
			new Offset<byte>(groupName, 25940)
		};
		MCP_BankLimitSel = new Offset<byte>(groupName, 25941);
		MCP_AltIncrSel = new Offset<byte>(groupName, 25942);
		MCP_DisengageBar = new Offset<byte>(groupName, 25943);
		MCP_Speed_Dial = new Offset<byte>(groupName, 25944);
		MCP_Heading_Dial = new Offset<byte>(groupName, 25945);
		MCP_Altitude_Dial = new Offset<byte>(groupName, 25946);
		MCP_VS_Wheel = new Offset<byte>(groupName, 25947);
		MCP_HDGDial_Mode = new Offset<byte>(groupName, 25948);
		MCP_VSDial_Mode = new Offset<byte>(groupName, 25949);
		MCP_AP_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25950),
			new Offset<byte>(groupName, 25951)
		};
		MCP_CLB_CON_Sw_Pushed = new Offset<byte>(groupName, 25952);
		MCP_AT_Sw_Pushed = new Offset<byte>(groupName, 25953);
		MCP_LNAV_Sw_Pushed = new Offset<byte>(groupName, 25954);
		MCP_VNAV_Sw_Pushed = new Offset<byte>(groupName, 25955);
		MCP_FLCH_Sw_Pushed = new Offset<byte>(groupName, 25956);
		MCP_HDG_HOLD_Sw_Pushed = new Offset<byte>(groupName, 25957);
		MCP_VS_FPA_Sw_Pushed = new Offset<byte>(groupName, 25958);
		MCP_ALT_HOLD_Sw_Pushed = new Offset<byte>(groupName, 25959);
		MCP_LOC_Sw_Pushed = new Offset<byte>(groupName, 25960);
		MCP_APP_Sw_Pushed = new Offset<byte>(groupName, 25961);
		MCP_Speeed_Sw_Pushed = new Offset<byte>(groupName, 25962);
		MCP_Heading_Sw_Pushed = new Offset<byte>(groupName, 25963);
		MCP_Altitude_Sw_Pushed = new Offset<byte>(groupName, 25964);
		MCP_IAS_MACH_Toggle_Sw_Pushed = new Offset<byte>(groupName, 25965);
		MCP_HDG_TRK_Toggle_Sw_Pushed = new Offset<byte>(groupName, 25966);
		MCP_VS_FPA_Toggle_Sw_Pushed = new Offset<byte>(groupName, 25967);
		MCP_annunAP = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25968),
			new Offset<byte>(groupName, 25969)
		};
		MCP_annunAT = new Offset<byte>(groupName, 25970);
		MCP_annunLNAV = new Offset<byte>(groupName, 25971);
		MCP_annunVNAV = new Offset<byte>(groupName, 25972);
		MCP_annunFLCH = new Offset<byte>(groupName, 25973);
		MCP_annunHDG_HOLD = new Offset<byte>(groupName, 25974);
		MCP_annunVS_FPA = new Offset<byte>(groupName, 25975);
		MCP_annunALT_HOLD = new Offset<byte>(groupName, 25976);
		MCP_annunLOC = new Offset<byte>(groupName, 25977);
		MCP_annunAPP = new Offset<byte>(groupName, 25978);
		DSP_L_INBD_Sw = new Offset<byte>(groupName, 25979);
		DSP_R_INBD_Sw = new Offset<byte>(groupName, 25980);
		DSP_LWR_CTR_Sw = new Offset<byte>(groupName, 25981);
		DSP_ENG_Sw = new Offset<byte>(groupName, 25982);
		DSP_STAT_Sw = new Offset<byte>(groupName, 25983);
		DSP_ELEC_Sw = new Offset<byte>(groupName, 25984);
		DSP_HYD_Sw = new Offset<byte>(groupName, 25985);
		DSP_FUEL_Sw = new Offset<byte>(groupName, 25986);
		DSP_AIR_Sw = new Offset<byte>(groupName, 25987);
		DSP_DOOR_Sw = new Offset<byte>(groupName, 25988);
		DSP_GEAR_Sw = new Offset<byte>(groupName, 25989);
		DSP_FCTL_Sw = new Offset<byte>(groupName, 25990);
		DSP_CAM_Sw = new Offset<byte>(groupName, 25991);
		DSP_CHKL_Sw = new Offset<byte>(groupName, 25992);
		DSP_COMM_Sw = new Offset<byte>(groupName, 25993);
		DSP_NAV_Sw = new Offset<byte>(groupName, 25994);
		DSP_CANC_RCL_Sw = new Offset<byte>(groupName, 25995);
		DSP_annunL_INBD = new Offset<byte>(groupName, 25996);
		DSP_annunR_INBD = new Offset<byte>(groupName, 25997);
		DSP_annunLWR_CTR = new Offset<byte>(groupName, 25998);
		WARN_Reset_Sw_Pushed = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 25999),
			new Offset<byte>(groupName, 26000)
		};
		WARN_annunMASTER_WARNING = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26001),
			new Offset<byte>(groupName, 26002)
		};
		WARN_annunMASTER_CAUTION = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26003),
			new Offset<byte>(groupName, 26004)
		};
		ISP_DsplCtrl_C_Sw_Altn = new Offset<byte>(groupName, 26005);
		LTS_UpperDsplBRIGHTNESSKnob = new Offset<byte>(groupName, 26006);
		LTS_LowerDsplBRIGHTNESSKnob = new Offset<byte>(groupName, 26007);
		EICAS_EventRcd_Sw_Pushed = new Offset<byte>(groupName, 26008);
		CDU_annunEXEC = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26009),
			new Offset<byte>(groupName, 26010),
			new Offset<byte>(groupName, 26011)
		};
		CDU_annunDSPY = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26012),
			new Offset<byte>(groupName, 26013),
			new Offset<byte>(groupName, 26014)
		};
		CDU_annunFAIL = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26015),
			new Offset<byte>(groupName, 26016),
			new Offset<byte>(groupName, 26017)
		};
		CDU_annunMSG = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26018),
			new Offset<byte>(groupName, 26019),
			new Offset<byte>(groupName, 26020)
		};
		CDU_annunOFST = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26021),
			new Offset<byte>(groupName, 26022),
			new Offset<byte>(groupName, 26023)
		};
		CDU_BrtKnob = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26024),
			new Offset<byte>(groupName, 26025),
			new Offset<byte>(groupName, 26026)
		};
		FCTL_AltnFlaps_Sw_ARM = new Offset<byte>(groupName, 26027);
		FCTL_AltnFlaps_Control_Sw = new Offset<byte>(groupName, 26028);
		FCTL_StabCutOutSw_C_NORMAL = new Offset<byte>(groupName, 26029);
		FCTL_StabCutOutSw_R_NORMAL = new Offset<byte>(groupName, 26030);
		FCTL_AltnPitch_Lever = new Offset<byte>(groupName, 26031);
		FCTL_Speedbrake_Lever = new Offset<byte>(groupName, 26032);
		FCTL_Flaps_Lever = new Offset<byte>(groupName, 26033);
		ENG_FuelControl_Sw_RUN = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26034),
			new Offset<byte>(groupName, 26035)
		};
		BRAKES_ParkingBrakeLeverOn = new Offset<byte>(groupName, 26036);
		COMM_SelectedMic = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26037),
			new Offset<byte>(groupName, 26038),
			new Offset<byte>(groupName, 26039)
		};
		COMM_ReceiverSwitches = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26040),
			new Offset<byte>(groupName, 26041),
			new Offset<byte>(groupName, 26042)
		};
		COMM_OBSAudio_Selector = new Offset<byte>(groupName, 26043);
		COMM_SelectedRadio = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26044),
			new Offset<byte>(groupName, 26045),
			new Offset<byte>(groupName, 26046)
		};
		COMM_RadioTransfer_Sw_Pushed = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26047),
			new Offset<byte>(groupName, 26048),
			new Offset<byte>(groupName, 26049)
		};
		COMM_RadioPanelOff = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26050),
			new Offset<byte>(groupName, 26051),
			new Offset<byte>(groupName, 26052)
		};
		COMM_annunAM = new Offset<byte>[3]
		{
			new Offset<byte>(groupName, 26053),
			new Offset<byte>(groupName, 26054),
			new Offset<byte>(groupName, 26055)
		};
		XPDR_XpndrSelector_R = new Offset<byte>(groupName, 26056);
		XPDR_AltSourceSel_ALTN = new Offset<byte>(groupName, 26057);
		XPDR_ModeSel = new Offset<byte>(groupName, 26058);
		XPDR_Ident_Sw_Pushed = new Offset<byte>(groupName, 26059);
		FIRE_EngineHandle = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26060),
			new Offset<byte>(groupName, 26061)
		};
		FIRE_EngineHandleUnlock_Sw = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26062),
			new Offset<byte>(groupName, 26063)
		};
		FIRE_annunENG_BTL_DISCH = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 26064),
			new Offset<byte>(groupName, 26065)
		};
		FCTL_AileronTrim_Switches = new Offset<byte>(groupName, 26066);
		FCTL_RudderTrim_Knob = new Offset<byte>(groupName, 26067);
		FCTL_RudderTrimCancel_Sw_Pushed = new Offset<byte>(groupName, 26068);
		EVAC_Command_Sw_ON = new Offset<byte>(groupName, 26069);
		EVAC_PressToTest_Sw_Pressed = new Offset<byte>(groupName, 26070);
		EVAC_HornSutOff_Sw_Pulled = new Offset<byte>(groupName, 26071);
		EVAC_LightIlluminated = new Offset<byte>(groupName, 26072);
		LTS_AisleStandPNLKnob = new Offset<byte>(groupName, 26073);
		LTS_AisleStandFLOODKnob = new Offset<byte>(groupName, 26074);
		LTS_FloorLightsSw = new Offset<byte>(groupName, 26075);
		DOOR_state = new Offset<byte>[16]
		{
			new Offset<byte>(groupName, 26076),
			new Offset<byte>(groupName, 26077),
			new Offset<byte>(groupName, 26078),
			new Offset<byte>(groupName, 26079),
			new Offset<byte>(groupName, 26080),
			new Offset<byte>(groupName, 26081),
			new Offset<byte>(groupName, 26082),
			new Offset<byte>(groupName, 26083),
			new Offset<byte>(groupName, 26084),
			new Offset<byte>(groupName, 26085),
			new Offset<byte>(groupName, 26086),
			new Offset<byte>(groupName, 26087),
			new Offset<byte>(groupName, 26088),
			new Offset<byte>(groupName, 26089),
			new Offset<byte>(groupName, 26090),
			new Offset<byte>(groupName, 26091)
		};
		ENG_StartValve = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27648),
			new Offset<byte>(groupName, 27649)
		};
		AIR_DuctPress = new Offset<float>[2]
		{
			new Offset<float>(groupName, 27652),
			new Offset<float>(groupName, 27656)
		};
		FUEL_QtyCenter = new Offset<float>(groupName, 27660);
		FUEL_QtyLeft = new Offset<float>(groupName, 27664);
		FUEL_QtyRight = new Offset<float>(groupName, 27668);
		FUEL_QtyAux = new Offset<float>(groupName, 27672);
		IRS_aligned = new Offset<byte>(groupName, 27676);
		AircraftModel = new Offset<byte>(groupName, 27677);
		WeightInKg = new Offset<byte>(groupName, 27678);
		GPWS_V1CallEnabled = new Offset<byte>(groupName, 27679);
		GroundConnAvailable = new Offset<byte>(groupName, 27680);
		FMC_TakeoffFlaps = new Offset<byte>(groupName, 27681);
		FMC_V1 = new Offset<byte>(groupName, 27682);
		FMC_VR = new Offset<byte>(groupName, 27683);
		FMC_V2 = new Offset<byte>(groupName, 27684);
		FMC_LandingFlaps = new Offset<byte>(groupName, 27685);
		FMC_LandingVREF = new Offset<byte>(groupName, 27686);
		FMC_CruiseAlt = new Offset<ushort>(groupName, 27688);
		FMC_LandingAltitude = new Offset<short>(groupName, 27690);
		FMC_TransitionAlt = new Offset<ushort>(groupName, 27692);
		FMC_TransitionLevel = new Offset<ushort>(groupName, 27694);
		FMC_PerfInputComplete = new Offset<byte>(groupName, 27696);
		FMC_DistanceToTOD = new Offset<float>(groupName, 27700);
		FMC_DistanceToDest = new Offset<float>(groupName, 27704);
		FMC_flightNumber = new Offset<string>(groupName, 27708, 9);
		AIR_AltnVentSw_ON = new Offset<byte>(groupName, 27717);
		AIR_annunAltnVentFAULT = new Offset<byte>(groupName, 27718);
		AIR_CargoTemp_MainDeckFwd_Sel = new Offset<byte>(groupName, 27719);
		AIR_CargoTemp_MainDeckAft_Sel = new Offset<byte>(groupName, 27720);
		AIR_CargoTemp_LowerFwd_Sel = new Offset<byte>(groupName, 27721);
		AIR_CargoTemp_LowerAft_Sel = new Offset<byte>(groupName, 27722);
		AIR_MainDeckFlowSw_NORM = new Offset<byte>(groupName, 27723);
		ELEC_annunAPU_GEN_OFF = new Offset<byte>(groupName, 27724);
		ELEC_annunBattery_OFF = new Offset<byte>(groupName, 27725);
		FCTL_PrimFltComputersSw_AUTO = new Offset<byte>(groupName, 27726);
		FCTL_annunPrimFltComputersDISC = new Offset<byte>(groupName, 27727);
		FIRE_EngineHandleIlluminated = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27728),
			new Offset<byte>(groupName, 27729)
		};
		FIRE_APUHandleIlluminated = new Offset<byte>(groupName, 27730);
		FIRE_EngineHandleIsUnlocked = new Offset<byte>[2]
		{
			new Offset<byte>(groupName, 27731),
			new Offset<byte>(groupName, 27732)
		};
		FIRE_APUHandleIsUnlocked = new Offset<byte>(groupName, 27733);
		FIRE_annunMainDeckCargoFire = new Offset<byte>(groupName, 27734);
		FIRE_annunCargoDEPR = new Offset<byte>(groupName, 27735);
		GPWS_RunwayOvrdSw_OVRD = new Offset<byte>(groupName, 27736);
		COMM_ReceiverSwitches_NEW = new Offset<ushort>[3]
		{
			new Offset<ushort>(groupName, 27738),
			new Offset<ushort>(groupName, 27740),
			new Offset<ushort>(groupName, 27742)
		};
		WheelChocksSet = new Offset<byte>(groupName, 27744);
		APURunning = new Offset<byte>(groupName, 27745);
	}

	~PMDG_777X_Offsets()
	{
		//FSUIPCConnection.DeleteGroup(groupName);
	}

	public void RefreshData()
	{
		RefreshData(0);
	}

	public void RefreshData(byte ClassInstance)
	{
		lock (lockObject)
		{
			if (FSUIPCConnection.IsConnectionOpenForClass(ClassInstance))
			{
				FSUIPCConnection.Process(ClassInstance, groupName);
				return;
			}
			if (ClassInstance == 0)
			{
				throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to FSUIPC is not open.");
			}
			throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to class instance " + ClassInstance.ToString("D2") + " of WideClient.exe is not open.");
		}
	}
}
