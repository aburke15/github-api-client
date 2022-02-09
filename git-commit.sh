echo "running git commit script"
git add .
echo "enter commit message:"
read message
git commit -m "$message"
echo "enter branch name:"
read branch
git push -u origin $branch
echo "changes committed to branch: $branch"