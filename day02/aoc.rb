puts "Ruby"
if ENV["part"] == "part1"
    scores = { :"A X" => 1+3, :"A Y" => 2+6, :"A Z" => 3+0, :"B X" => 1+0, :"B Y" => 2+3, :"B Z" => 3+6, :"C X" => 1+6, :"C Y" => 2+0, :"C Z" => 3+3 }
    puts File.readlines("input.txt", chomp: true).map{|line| scores[line.to_sym]}.sum
elsif ENV["part"] == "part2"
    scores = { :"A X" => 3+0, :"A Y" => 1+3, :"A Z" => 2+6, :"B X" => 1+0, :"B Y" => 2+3, :"B Z" => 3+6, :"C X" => 2+0, :"C Y" => 3+3, :"C Z" => 1+6 }
    puts File.readlines("input.txt", chomp: true).map{|line| scores[line.to_sym]}.sum
end