Transform /^center of the map$/ do |something|
	'25_25'
end

Transform /^location "(.*?)"$/ do |coordinates|  
	coordinates[", "] = "_"
	coordinates
end

#Transform /^location "(.*?)"$/ do |coordinates|
#	coordinates[","] = "_"
#	coordinates
#end

Transform /^display an obstacle$/ do |name|
	"Obstacle.png"
end

Transform /^default obstacles$/ do |foo|
	on(MissionPage)
end