import json, requests, argparse

parser = argparse.ArgumentParser()
parser.add_argument('token')
parser.add_argument('botname')
parser.add_argument('roomurl')
parser.add_argument('directurl')
parser.add_argument('webexurl')

args = parser.parse_args()

# Clean up previous hooks
print('Cleaning up previous hooks\n')

response = requests.get(
    "%s/v1/webhooks" %(args.webexurl),
    headers={'Authorization': "Bearer %s" %(args.token)}
)

json_response = response.json()
print(json.dumps(json_response))

for webhook in json_response['items']:
    requests.delete(
        "%s/v1/webhooks/%s" %(args.webexurl, webhook['id']),
        headers={'Authorization': "Bearer %s" %(args.token)}
    )

# Create room webhook
print('Creating room webhook')

room_body = {
    'name': "%s Webhook" %(args.botname),
    'targetUrl': args.roomurl,
    'resource': 'messages',
    'event': 'created',
    'filter': 'mentionedPeople=me'
}

requests.post(
    "%s/v1/webhooks" %(args.webexurl),
    headers={'Authorization': "Bearer %s" %(args.token)},
    data=room_body
)

# Create direct webhook
print('Creating direct webhook')

direct_body = {
    'name': "%s Direct Webhook" %(args.botname),
    'targetUrl': args.directurl,
    'resource': 'messages',
    'event': 'created',
    'filter': 'mentionedPeople=me'
}

requests.post(
    "%s/v1/webhooks" %(args.webexurl),
    headers={'Authorization': "Bearer %s" %(args.token)},
    data=direct_body
)
