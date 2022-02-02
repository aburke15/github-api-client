echo "Running git commit script"
git add .
echo "Enter commit message"
read message
git commit -m "$message"
echo "Enter branch name"
read branch
git push -u origin $branch