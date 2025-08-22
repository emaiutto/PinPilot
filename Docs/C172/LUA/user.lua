function BRAKES()
	ipc.control(65588)
end

function A172_Beacon_And_NAV_Light_toggle()
    Lights_BEACON_toggle()
    Lights_NAV_toggle()
end

function HEADING_BUG_INC()
	ipc.control(65879)
--	ipc.sleep(50)
end

function HEADING_BUG_DEC()
	ipc.control(65880)
--	ipc.sleep(50)
end

function VOR1_OBI_INC()
	ipc.control(65663)
	--ipc.control(65663)
end

function VOR1_OBI_DEC()
	ipc.control(65662)
	--ipc.control(65662)
end

function ADF_CARD_INC()
	ipc.control(65881)
	--ipc.control(65881)
end

function ADF_CARD_DEC()
	ipc.control(65882)
	--ipc.control(65882)
end
