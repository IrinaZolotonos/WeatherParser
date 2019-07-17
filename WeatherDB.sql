CREATE TABLE test.cities (
  id int(11) NOT NULL AUTO_INCREMENT,
  name varchar(50) NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 104,
AVG_ROW_LENGTH = 682,
CHARACTER SET cp1251,
COLLATE cp1251_general_ci;


CREATE TABLE test.weather (
  city_id int(11) NOT NULL,
  weather_date date NOT NULL DEFAULT 'curdate()',
  degrees_day int(11) NOT NULL DEFAULT 0,
  degrees_night int(11) NOT NULL DEFAULT 0
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 682,
CHARACTER SET cp1251,
COLLATE cp1251_general_ci;

ALTER TABLE test.weather
ADD CONSTRAINT FK_weather_city_id FOREIGN KEY (city_id)
REFERENCES test.cities (id) ON DELETE CASCADE ON UPDATE CASCADE;


CREATE DEFINER = 'root'@'localhost'
PROCEDURE test.AddCityWeather(IN in_city VARCHAR(255) COLLATE cp1251_general_ci, IN in_degreeDay INT, IN in_degreeNight INT)
BEGIN
  
  DECLARE cur_id int;
  DECLARE cur_date date;
  SELECT date(DATE_ADD(NOW(), INTERVAL 1 DAY)) INTO cur_date;

  IF NOT EXISTS (SELECT id FROM cities c WHERE name LIKE in_city) then
       INSERT cities (name) VALUE (in_city);
       SELECT Last_insert_id() INTO cur_id;
  ELSE
       SELECT id INTO cur_id FROM cities c WHERE name LIKE in_city;
  END IF;

  IF EXISTS (SELECT city_id FROM weather w WHERE w.city_id = cur_id) THEN
       UPDATE weather w 
       SET w.degrees_day = in_degreeDay, w.degrees_night = in_degreeNight, w.weather_date = cur_date 
       WHERE w.city_id = cur_id;
   ELSE
       INSERT weather (city_id, weather_date, degrees_day, degrees_night)
       VALUE (cur_id, cur_date, in_degreeDay, in_degreeNight);
   END IF;

END;

CREATE DEFINER = 'root'@'localhost'
PROCEDURE test.GetCities()
BEGIN
  SELECT id, name FROM cities c ORDER BY name;
END;


CREATE DEFINER = 'root'@'localhost'
PROCEDURE test.GetCityDegrees(IN id INT)
BEGIN
  SELECT w.degrees_day, w.degrees_night FROM weather w WHERE city_id = id AND w.weather_date =  date(DATE_ADD(NOW(), INTERVAL 1 DAY)) ;
END;